using System.Windows.Input;
using Xamarin.Forms;

namespace Xde.App.ViewModels
{
	public class WaterViewModel : BaseViewModel
    {
        public WaterViewModel()
        {
            Title = "Water";
			DrinkCommand = new Command(DrinkHandler);
		}

		private async void DrinkHandler()
		{
			await Application.Current.MainPage.DisplayAlert("Drink confirm", $"Drink {Volume}ml?", "Yes", "No");
		}

		public int Volume { get; set; } = DefaultAmount;

        public ICommand DrinkCommand { get; }

		public const int DefaultAmount = 300;
    }
}
