using System.Windows.Input;
using ReactiveUI;
using Xamarin.Forms;
using Xde.App.Services.Spins;
using Xde.App.Views.Spins;

namespace Xde.App.ViewModels.Spins
{
	//TODO:public class MainViewModel : BaseViewModel
	public class MainViewModel : ReactiveObject
	{
		public ISpinService Spins { get; init; }

		public ICommand SearchCommand { get; }

		public bool SearchMode { get; set; } = false;

		public MainViewModel(ISpinService spins)
		{
			Spins = spins;

			SearchCommand = new Command(SearchHandler);
		}

		private async void SearchHandler(object obj)
		{
			var searchPage = new SearchPage(this);
			//TODO:searchPage.BindingContext = ...
			await Application.Current.MainPage.Navigation.PushModalAsync(searchPage, true);
		}
	}
}
