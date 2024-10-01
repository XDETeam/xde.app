using System;
using System.Security.Cryptography;
using System.Text;

namespace Xde.Forms.Schema
{
	/// <summary>
	/// Full name
	/// </summary>
	/// 
	/// <remarks>
	/// TODO:Simple basics that defines name that can be uniquely distinguished from the
	/// other one.
	/// </remarks>
	public class Fullname
	{
		/// <summary>
		/// Short name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Namespace
		/// </summary>
		public string Namespace { get; set; }

		/// <summary>
		/// Layer
		/// </summary>
		/// <remarks>
		/// TODO: Experiment with introducing layers as an additional axis of naming.
		/// For example, .NET class can have name (<see cref="Name"/>), namespace
		/// <see cref="Namespace"/> and assembly that is a very good candidate to be
		/// a layer.
		/// </remarks>
		public string Layer { get; set; }

		/// <summary>
		/// TODO:Fullname hash
		/// </summary>
		/// 
		/// <remarks>
		/// It would be good to have some short Id for the <see cref="Fullname"/>.
		/// This is temporary solution using SHA2. Not optimal, probably cache would be also
		/// good. Maybe <see cref="object.GetHashCode()"/> will be enough. Let's say
		/// codes for Name/Namespace/Layer will be joined into long, Guid, ...
		/// </remarks>
		public string Hash => Convert.ToBase64String(
			SHA256.HashData(
				Encoding.UTF8.GetBytes($"{Namespace}.{Name}.{Layer}")
			)
		);
	}
}
