using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            BindingContext = new NodeReviewViewModel();
        }
    }
}