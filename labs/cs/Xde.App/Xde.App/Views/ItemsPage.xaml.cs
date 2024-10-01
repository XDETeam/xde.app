using Xamarin.Forms;
using Xde.App.ViewModels;

namespace Xde.App.Views
{
	public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

			BindingContext = _viewModel = DependencyService.Resolve<ItemsViewModel>();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}