using MonitoringPU.ViewModels;
using MonitoringPU.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonitoringPU.Models
{

    public delegate void ChangeNilai(double val);
    public class itempenilaian :BaseViewModel
   {
        public itempenilaian()
        {
            AddPhotoCommand = new Command(AddPhotoAction);
        }
        public event ChangeNilai OnChangeNilai;
        private double _nilai;
        private double _progress;
        private double _progressView;

        public int ItemPenilaianId {  get; set;} 

          public int AspekPenialainId {  get; set;} 

          public double Nilai {
            get { return _nilai; }
            set {
                SetProperty(ref _nilai,value);
                Progress = 0;
                ProgressView = 0;
                if (OnChangeNilai != null)
                    OnChangeNilai(value);
            }
            } 

          public string Keterangan {  get; set;} 

          public int Periode {  get; set;}

        public aspekpenilaian Aspek { get; set; }


        public double NilaiView
        {
            get { return Nilai / 100; }
        }

        public double Progress
        {
            get{return Aspek != null ? (Nilai * Aspek.BobotPenilaian) / 100 : 0;}
            set { SetProperty(ref _progress, value); }
        }

        public double ProgressView
        {
            get
            {
                return Progress / 100;
            }
            set { SetProperty(ref _progressView, value); }
        }

        public double Minimum { get; set; }
        public Command AddPhotoCommand { get; }

        private async void AddPhotoAction(object obj)
        {
            //var app = await Helper.GetBaseApp();
            //app.ChangeScreen(new TakeFotoView(obj));
            try
            {
                await CrossMedia.Current.Initialize();

                //var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                //var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                //if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                //{
                //    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                //    cameraStatus = results[Permission.Camera];
                //    storageStatus = results[Permission.Storage];
                //}

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                  Helper.ShowMessageError("No Camera");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Fotos",
                    Name = "test.jpg",
                    AllowCropping = true,
                    PhotoSize = PhotoSize.Small
                });

                if (file == null)
                    return;

                //  await DisplayAlert("File Location", file.Path, "OK");
               
               var Foto = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                  
                    return stream;
                });

                var data = new foto { ItemPenilaianId = ItemPenilaianId,Foto=Helper.ReadFully(file.GetStream()) };
                if (Foto != null)
                    Projects.AddNewFoto(data);
            }
            catch (Exception ex)
            {

                Helper.ShowMessageError(ex.Message);
            }
        }

    }
}


