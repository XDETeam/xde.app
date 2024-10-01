using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xde.App.Services;

namespace Xde.App.ViewModels.Settings
{
	public class MainViewModel : BaseViewModel
	{
		private readonly IAppSettings _settings;

		public ICommand CancelCommand { get; }
		public ICommand UpdateCommand { get; }

		private string _connectionString = DefaultConnectionString;

		public string ConnectionString
		{
			get => _connectionString;
			set => SetProperty(ref _connectionString, value);
		}

		private void InitModel()
		{
			ConnectionString = _settings.ConnectionString;
		}

		public MainViewModel(IAppSettings settings)
		{
			_settings = settings;

			InitModel();

			CancelCommand = new Command(CancelHandler);
			UpdateCommand = new Command(UpdateHandler);
		}

		private void CancelHandler(object obj)
		{
			InitModel();
		}

		private async void UpdateHandler(object obj)
		{
			_settings.ConnectionString = ConnectionString;

			await Task.CompletedTask;
		}

		public const string DefaultConnectionString = "Host=my-server;Port=16328;Username=xde_app;Password=!qa2Ws3eD;Database=xde";
	}
}
