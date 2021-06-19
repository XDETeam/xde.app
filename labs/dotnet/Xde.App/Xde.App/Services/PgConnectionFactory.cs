using Npgsql;
using System.Data;

namespace Xde.App.Services
{
	public class PgConnectionFactory
		: IDbConnectionFactory
	{
		IDbConnection IDbConnectionFactory.CreateConnection()
		{
			const string connString = "";

			return new NpgsqlConnection(connString);
		}
	}
}
