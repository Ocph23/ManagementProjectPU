using System;
using System.Collections.Generic;
using System.Text;

namespace MonitoringPU
{
   public class AuthenticationToken
    {
        public List<string> roles { get; set; }
        public string token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string Email { get; set; }
    }
}
