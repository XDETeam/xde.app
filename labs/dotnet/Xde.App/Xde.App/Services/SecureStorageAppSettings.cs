using Xamarin.Essentials;

namespace Xde.App.Services
{
	public class SecureStorageAppSettings : IAppSettings
	{
		string IAppSettings.ConnectionString
		{
			get
			{
				return SecureStorage.GetAsync(KeyConnectionString).Result;
			}
			set
			{
				SecureStorage.SetAsync(KeyConnectionString, value).Wait();
			}
		}

		public const string KeyConnectionString = "xde.connection.string";
	}
}
