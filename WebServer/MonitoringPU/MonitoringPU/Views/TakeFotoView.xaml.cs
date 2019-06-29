using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
	public partial class TakeFotoView : ContentPage
	{
		public TakeFotoView (object obj)
		{
			InitializeComponent ();
            this.BindingContext = this;
		}

        async void TakePhoto_Clicked(object sender, System.EventArgs e)
        {
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
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Fotos",
                    Name = "test.jpg"   , AllowCropping=true, PhotoSize= PhotoSize.Small
                });

                if (file == null)
                    return;

              //  await DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            catch (Exception ex)
            {

                Helper.ShowMessageError(ex.Message);
            }
        }
    }
}