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
	public partial class PenilaianView : ContentPage
	{
		public PenilaianView (Project item)
		{
			InitializeComponent ();
            BindingContext = new PenilaianViewModel(item);
        }
	}



    public class PenilaianViewModel : BaseViewModel
    {
        private Project _projectSelected;

        public Project ProjectSelected
        {
            get { return _projectSelected; }
            set
            {
                SetProperty(ref _projectSelected, value);
            }

        }
        public PenilaianViewModel(Project item)
        {
            ProjectSelected = item;
        }
    }
}