using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xde.App.ViewModels.Spins;

namespace Xde.App.Views.Spins
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