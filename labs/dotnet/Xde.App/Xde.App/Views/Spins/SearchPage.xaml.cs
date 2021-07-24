using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xde.App.ViewModels.Spins;

namespace Xde.App.Views.Spins
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		private readonly MainViewModel _mainViewModel;
		private readonly SearchViewModel _viewModel;

		public SearchPage(MainViewModel mainViewModel)
		{
			InitializeComponent();

			_mainViewModel = mainViewModel;
			_viewModel = new SearchViewModel(_mainViewModel);
		}
	}
}