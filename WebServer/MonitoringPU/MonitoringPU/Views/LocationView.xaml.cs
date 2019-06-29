using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MonitoringPU.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationView : ContentPage
    {
        public LocationView(Models.project param)
        {
            InitializeComponent();
            webView.Source = param.Map;
            BindingContext = new LocationViewModel();

        }
    }


    public class LocationViewModel : BaseNotify
    {
        public ObservableCollection<Pin> Pins { get; set; }
        private Position myPosition;

        public Position MyPosition
        {
            get { return myPosition; }
            set { SetProperty(ref myPosition, value); }
        }

        public LocationViewModel()
        {
            Pins = new ObservableCollection<Pin>();
            Pins.Add(new Pin() { Label = "Home", Position = new Position(-2.612852, 140.678479) });
            MyPosition = new Position(-2.612852, 140.678479);
        }

    }
}