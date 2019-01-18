using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MonitoringPU.Views;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MonitoringPU
{
    public partial class App : Application
    {
        public AuthenticationToken Token { get; set; }

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();

            MessagingCenter.Subscribe<MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ChangeScreen(Page page)
        {
            Current.MainPage = new NavigationPage( page);
        }

        internal void SetToken(AuthenticationToken token)
        {
            this.Token = token;
        }

        internal Task<AuthenticationToken> GetToken()
        {
            return Task.FromResult(Token);
        }

    }


    public class MessagingCenterAlert
    {
      
        public static void Init()
        {
            var time = DateTime.UtcNow;
        }

      
        public string Title { get; set; }

       
        public string Message { get; set; }

       
        public string Cancel { get; set; }

       
        public Action OnCompleted { get; set; }
    }
}
