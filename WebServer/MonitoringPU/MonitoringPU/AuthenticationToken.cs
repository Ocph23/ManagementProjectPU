using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringPU
{
    public class AuthenticationToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public string Roles { get; set; }
    }


    public class ErrorMessage
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
