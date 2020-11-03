using System.ComponentModel;
using Xamarin.Forms;
using Xde.App.ViewModels;

namespace Xde.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}