using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MonitoringPU.ViewModels;
using Xamarin.Forms;
using System.IO;

namespace MonitoringPU
{
    public class Helper
    {

        
      //  private static string _server = "http://192.168.1.2/";
        private static string _server = "http://monitoringproyek.gear.host/";

        public static async Task<AuthenticationToken> GetToken()
        {
            var app = ((App)App.Current);
            return await Task.FromResult(await app.GetToken());
        }


        public static Task<App> GetBaseApp()
        {
            var app = ((App)App.Current);
            return Task.FromResult(app);
        }

     
        public static AuthenticationToken Token { get; set; }
        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }


        internal static void ShowMessageError(string v)
        {
            MessagingCenter.Send(new MessagingCenterAlert
            {
                Title = "Error",
                Message = v,
                Cancel = "OK"
            }, "message");

        }

        internal static void ShowMessage(string v)
        {
            MessagingCenter.Send(new MessagingCenterAlert
            {
                Title = "Info",
                Message = v,
                Cancel = "OK"
            }, "message");

        }


        public static StringContent Content(object str)
        {
            var value = JsonConvert.SerializeObject(str);
            return new StringContent(value, Encoding.UTF8, "application/json");
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }

        }
    }


   
}
