using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace MonitoringPU.Models
{
    public class aspekpenilaian
    {
        public int AspekPenialainId { get; set; }

        public int ProjekId { get; set; }

        public int Urutan { get; set; }

        public int KonsultanId { get; set; }

        public string Aspek { get; set; }

        public double BobotPenilaian { get; set; }

        public string Keterangan { get; set; }

    }
}


