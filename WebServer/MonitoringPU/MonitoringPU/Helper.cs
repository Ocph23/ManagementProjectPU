using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringPU
{
   public class Helper
    {
        public static Task<App> GetBaseApp()
        {
            var app = ((App)App.Current);
            return Task.FromResult(app);
        }

    }
}
