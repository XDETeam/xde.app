using Xamarin.Forms;
using Xde.App.Models;
using Xde.App.ViewModels;

namespace Xde.App.Views
{
	public partial class NodeReviewPage : ContentPage
    {
        public Item Item { get; set; }

        public NodeReviewPage()
        {
            InitializeComponent();

			BindingContext = DependencyService.Resolve<NodeReviewViewModel>();
        }
    }
}