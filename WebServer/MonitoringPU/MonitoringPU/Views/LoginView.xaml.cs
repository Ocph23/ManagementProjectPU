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
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();
            BindingContext = new LoginViewModel();
		}
    }


    public class LoginViewModel :BaseViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new Command(LoginAction);
            Email = "matiusmorin85@gmail.com";
           // Password = "vCfRAp3#";
            Password = "uht5rY3#";
        }

        private async void LoginAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                if (IsValid)
                {
                    var result = await AuthDataService.Login(Email, Password);
                    if (result != null)
                    {
                        var main = await Helper.GetBaseApp();
                        main.Token = result;
                        main.ChangeScreen(new ItemsPage());
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set {SetProperty(ref email ,value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }



        private bool IsValid
        {
            get
            {
                return string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email) ? false : true;
            }
        }

        public Command LoginCommand { get; }
    }
}