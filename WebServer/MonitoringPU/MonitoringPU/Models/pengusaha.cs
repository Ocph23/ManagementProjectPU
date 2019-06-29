using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace MonitoringPU.Models
{
    public class pengusaha
    {
        public int PengusahaId { get; set; }

        public string Nama { get; set; }

        public string Direktur { get; set; }

        public string Alamat { get; set; }

        public string Telepon { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public IEnumerable<project> Projects { get; internal set; }
    }
}


