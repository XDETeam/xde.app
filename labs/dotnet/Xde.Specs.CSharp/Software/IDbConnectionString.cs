namespace Xde.Software
{
	/// <summary>
	/// TODO:
	/// </summary>
	/// 
	/// <remarks>
	/// There is an idea to have some basic abstraction for DBMS and maybe other
	/// storages connection. Host, port, user, password, database name, etc.
	/// 
	/// Maybe think about more straightforward implementation. E.g. Address/Uri property
	/// that combines host, port, schema(transport), query parameters into
	/// https://datatracker.ietf.org/doc/html/rfc3986. For example as it works for
	/// Postgres: "postgresql://localhost/mydb?user=other&amp;password=secret".
	/// Both solution can be applicable and from/to URI converters exists. E.g. some
	/// ISupportUriFormat for the factory.
	/// 
	/// It's not only for databases. Any network service.
	/// 
	/// Schema can be used to identify provider. Something like that is used in ActiveMQ.
	/// Schemas can be combination of sub-schemas, e.g. "amq+wss:...."
	/// </remarks>
	public interface IDbConnectionString
	{
		public string Server { get; set; }

		public int Port { get; set; }

		public string User { get; set; }

		public string Password { get; set; }
	}
}
