using System;
using System.Buffers;
using System.Buffers.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Xde.Software.Postgres
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// This experiment is targeting many goals:
	/// - Explore working with structured streams
	/// - Extend such structured streams to be employed in (de)serialization
	/// - Share the same solution for different DBMS/MQ/EP/etc
	/// - Consider DB negotiation to be part of event-based flow
	/// 
	/// - It would be interesting to make some benchmarks and measure timings for
	/// different negotiation stages.
	/// - Pooling (probably shared for the shared connection string structure)
	/// - https://devblogs.microsoft.com/dotnet/system-io-pipelines-high-performance-io-in-net/
	/// - Possibility to create custom "commands" for Postgres protocol or easily extend
	/// - Actively use spans for reading/writing data?
	/// </remarks>
	public class PgClient
	{
		private readonly string _address;
		private readonly int _port;
		private readonly string _user;
		private readonly string _password;

		public PgClient(string address, int hostPort, string pgUser, string pgPassword)
		{
			_address = address;
			_port = hostPort;
			_user = pgUser;
			_password = pgPassword;
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <remarks>
		/// Many applications make a lot of runtime check for every Int32 written.
		/// Probably it's better to create one instance for platform encodder.
		/// It can be interface or... static methods.
		/// 
		/// Name is definitely not good.
		/// </remarks>
		public interface IPlatformEncoder
		{
			void Write(Span<byte> span, int value);
		}

		public class LittleEndianEncoder : IPlatformEncoder
		{
			void IPlatformEncoder.Write(Span<byte> span, int value)
				=> BinaryPrimitives.WriteInt32LittleEndian(span, value)
			;
		}

		public class BigEndianEncoder : IPlatformEncoder
		{
			void IPlatformEncoder.Write(Span<byte> span, int value)
				=> BinaryPrimitives.WriteInt32BigEndian(span, value)
			;
		}

		public void Connect()
		{
			IPlatformEncoder encoder = BitConverter.IsLittleEndian
				? new LittleEndianEncoder()
				: new BigEndianEncoder()
			;

			var ipHost = Dns.GetHostEntry(_address);
			var ipAddress = ipHost.AddressList[0];
			var remoteEndpoint = new IPEndPoint(ipAddress, _port);

			using var client = new TcpClient();
			client.Connect(remoteEndpoint);

			// TODO: https://www.postgresql.org/docs/current/protocol.html
			
			// TODO:Probably better to render parameters separately to employ
			// strings reusage? Or use string.Create.
			var startupArgs = $"user\0{_user}\0database\0{_user}\0";
			var length = sizeof(int) // Length
				+ sizeof(int) // Protocol version
				+ startupArgs.Length
				+ sizeof(byte) // TODO:Trailing zero?
			;
			var protocolVersion = 3 << 16; // Protocol v3

			var buffer = ArrayPool<byte>.Shared.Rent(length).AsSpan();

			BinaryPrimitives.WriteInt32BigEndian(buffer[0..], length);
			// TODO:Protocol can be written as major version (3) and then minor version (0)
			BinaryPrimitives.WriteInt32BigEndian(buffer[4..], protocolVersion);
			Encoding.UTF8.GetBytes(startupArgs).CopyTo(buffer[8..]);
			buffer[length - 1] = 0;

			using var stream = client.GetStream();

			stream.Write(buffer.ToArray(), 0, buffer.Length);

			var response = ArrayPool<byte>.Shared.Rent(1024);
			var test1 = stream.Read(response, 0, response.Length);
		}
	}
}
