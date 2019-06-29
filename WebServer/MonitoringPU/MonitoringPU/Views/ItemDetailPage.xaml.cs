using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MonitoringPU.Models;
using MonitoringPU.ViewModels;

namespace MonitoringPU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

           
            BindingContext = viewModel;
        }
    }
}