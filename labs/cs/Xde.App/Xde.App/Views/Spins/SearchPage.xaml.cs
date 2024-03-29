using System.Diagnostics;
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

			listView.ItemsSource = _mainViewModel.Spins.Search(searchBar.Text);

			listView.ItemTapped += async (sender, e) =>
			{
				await DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
				Debug.WriteLine("Tapped: " + e.Item);
				((ListView)sender).SelectedItem = null; // de-select the row
			};
		}
	}
}