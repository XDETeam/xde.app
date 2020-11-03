using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xde.App.ViewModels
{
	public class WaterViewModel : BaseViewModel
    {
        public WaterViewModel()
        {
            Title = "Water";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

		public int Volume { get; set; } = DefaultAmount;

        public ICommand OpenWebCommand { get; }

		public const int DefaultAmount = 300;
    }
}