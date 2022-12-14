using System.Data;

namespace Xde.App.Services
{
	public interface IDbConnectionFactory
	{
		IDbConnection CreateConnection();
	}
}
