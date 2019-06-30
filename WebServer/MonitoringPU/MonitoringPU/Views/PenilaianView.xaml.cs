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
		public PenilaianView (project item)
		{
			InitializeComponent ();
            BindingContext = new PenilaianViewModel(item);
        }
	}


    public class PenilaianViewModel : BaseViewModel
    {
        private project _projectSelected;
        private Periode _periode;
        private Command _saveCommand;

        public project ProjectSelected
        {
            get { return _projectSelected; }
            set
            {
                SetProperty(ref _projectSelected, value);
            }

        }

        public Command NewPeriodeCommand { get; }

        public Command SaveCommand {
            get { return _saveCommand; }
            set { SetProperty(ref _saveCommand, value); }
        }

        public Command AddPhotoCommand { get; }

        public PenilaianViewModel(project item)
        {
           
            ProjectSelected = item;
            NewPeriodeCommand = new Command(CreateNewPerode, CreateNewPeriodeValidate);
            SaveCommand = new Command(SaveCommandAction,x=>PeriodeSelected!=null);
            if (ProjectSelected != null && ProjectSelected.Periodes != null)
                PeriodeSelected = ProjectSelected.Periodes.FirstOrDefault();
            Load();
        }

        private async void Load()
        {
            await Task.Delay(200);
            var main = await Helper.GetBaseApp();
           if(main!=null && main.Token!=null && main.Token.Roles=="Konsultan")
            {
                IsKonsultan = true;
            }
        }

        private async void SaveCommandAction(object obj)
        {
            try
            {
               Periode result = await Projects.SavePenilaian(PeriodeSelected,ProjectSelected.ProjekId);
                if(result!=null)
                {
                    foreach(var item in result.Data)
                    {
                        var data = PeriodeSelected.Data.Where(O => O.AspekPenialainId == item.AspekPenialainId).FirstOrDefault();
                        if (data != null)
                            data.ItemPenilaianId = item.ItemPenilaianId;
                    }
                }
            }
            catch (Exception ex)
            {

                Helper.ShowMessageError(ex.Message);
            }
        }

        private bool CreateNewPeriodeValidate(object arg)
        {
            return ProjectSelected != null;
        }

        private async void CreateNewPerode(object obj)
        {
            try
            {
                await Task.Delay(200);
                Periode periode = new Periode();
                periode.Data = new List<itempenilaian>();

                if (ProjectSelected.LastPeriode != null)
                {
                    var lastId = ProjectSelected.LastPeriode.PeriodeId + 1;
                    periode.PeriodeId = lastId;
                    foreach (var item in ProjectSelected.LastPeriode.Data)
                    {
                        double min = 0;
                        if (item.Nilai < 100)
                            min = item.Nilai;
                        var nilai = new itempenilaian { Aspek = item.Aspek, AspekPenialainId = item.AspekPenialainId, Periode = lastId, Minimum = min, Nilai=item.Nilai };
                        periode.Data.Add(nilai);
                    }
                }
                else
                {
                    periode.PeriodeId = 1;
                    foreach (var item in ProjectSelected.AspekPenilaian)
                    {
                        var nilai = new itempenilaian { Aspek = item, AspekPenialainId = item.AspekPenialainId, Periode = 1, Minimum = 0 };
                        periode.Data.Add(nilai);
                    }
                }

                ProjectSelected.Periodes.Add(periode);
                PeriodeSelected = periode;
            }
            catch (Exception ex)
            {

                Helper.ShowMessageError(ex.Message);
            }
         
        }

        public Periode PeriodeSelected
        {
            get { return _periode; }
            set
            {
                SetProperty(ref _periode, value);
                if(value!=null)
                {
                    _periode.InstanceEventChangeValue();
                }
                SaveCommand = new Command(SaveCommandAction, x => PeriodeSelected != null);
            }
        }


        private bool _IsKonsultan;

        public bool IsKonsultan
        {
            get { return _IsKonsultan; }
            set {SetProperty(ref _IsKonsultan , value); }
        }

    }
}