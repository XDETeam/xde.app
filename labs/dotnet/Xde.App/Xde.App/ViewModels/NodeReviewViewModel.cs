using System.Windows.Input;
using Xamarin.Forms;

namespace Xde.App.ViewModels
{
	public class NodeReviewViewModel : BaseViewModel
    {
        public NodeReviewViewModel()
        {
            Title = "Node Review";
			NodeReviewCommand = new Command(NodeReviewHandler);
		}

		private async void NodeReviewHandler()
		{
			await Application.Current.MainPage.DisplayAlert("confirm", "?", "Yes", "No");
		}

		public ICommand NodeReviewCommand { get; }

		public int Content { get; set; } = DefaultContent;

		public const string DefaultContent = "<some prop=\"val\">test</some>";
    }
}
