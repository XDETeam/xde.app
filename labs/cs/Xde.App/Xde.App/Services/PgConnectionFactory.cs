using Npgsql;
using System.Data;

namespace Xde.App.Services
{
	public class PgConnectionFactory
		: IDbConnectionFactory
	{
		private readonly IAppSettings _settings;

		public PgConnectionFactory(IAppSettings settings)
		{
			_settings = settings;
		}

		IDbConnection IDbConnectionFactory.CreateConnection()
		{
			return new NpgsqlConnection(_settings.ConnectionString);
		}
	}
}
