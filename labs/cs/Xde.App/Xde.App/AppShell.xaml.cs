using System;
using System.Linq;
using Xamarin.Forms;
using Xde.App.Views;

namespace Xde.App
{
	public partial class AppShell : Shell
	{
        public AppShell()
        {
            InitializeComponent();

			//TODO:
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

			//TODO:
			CurrentItem = Items.Single(item => item.Title == "Spins");
		}

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
			// TODO: await Shell.Current.GoToAsync("//LoginPage");
		}
    }
}
