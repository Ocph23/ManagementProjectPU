using MonitoringPU.Models;
using MonitoringPU.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonitoringPU.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressView : ContentPage
	{
		public ProgressView (project item)
		{
			InitializeComponent ();
            BindingContext = new ProgressViewModel(item);
		}

       
    }


    public class ProgressViewModel:BaseViewModel
    {
        private project _projectSelected;

        public project ProjectSelected {
            get { return _projectSelected; }
            set
            {
                SetProperty(ref _projectSelected, value);
            }

        }
        public ProgressViewModel(project item)
        {
            ProjectSelected = item;
        }
    }
}