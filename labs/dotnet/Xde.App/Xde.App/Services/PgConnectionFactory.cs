using System.Data;

namespace Xde.App.Services
{
	public class PgConnectionFactory
		: IDbConnectionFactory
	{
		public PgConnectionFactory(PgSettings settings)
		{

		}

		IDbConnection IDbConnectionFactory.CreateConnection()
		{
			throw new System.NotImplementedException(); //TODO:0
		}
	}
}
