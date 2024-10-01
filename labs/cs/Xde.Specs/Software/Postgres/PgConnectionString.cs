namespace Xde.Software.Postgres
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// Experiment with shared abstraction.
	/// 
	/// Collect settings from Postgres documentation.
	/// </remarks>
	public class PgConnectionString
		: IDbConnectionString
	{
		string IDbConnectionString.Server
		{
			get => Host;
			set => Host = value;
		}

		int IDbConnectionString.Port
		{
			get => Port;
			set => Port = value;
		}

		string IDbConnectionString.User
		{
			get => Username;
			set => Username = value;
		}

		string IDbConnectionString.Password
		{
			get => Password;
			set => Password = value;
		}

		public string Host { get; set; }

		public int Port { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
