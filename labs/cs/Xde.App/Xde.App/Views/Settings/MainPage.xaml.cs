using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xde.App.ViewModels.Settings;

namespace Xde.App.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		private readonly MainViewModel _viewModel;

		public MainPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = DependencyService.Resolve<MainViewModel>();
		}
	}
}