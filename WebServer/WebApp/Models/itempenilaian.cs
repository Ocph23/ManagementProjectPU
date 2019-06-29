using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace WebApp.Models
{
    [TableName("itempenilaian")]
    public class itempenilaian
    {
        [PrimaryKey("ItemPenilaianId")]
        [DbColumn("ItemPenilaianId")]
        public int ItemPenilaianId { get; set; }

        [DbColumn("AspekPenialainId")]
        public int AspekPenialainId { get; set; }



        [DbColumn("Nilai")]
        public double Nilai { get; set; }

        [DbColumn("Keterangan")]
        public string Keterangan { get; set; }

        [DbColumn("Periode")]
        public int Periode { get; set; }
        public aspekpenilaian Aspek { get; internal set; }

        public double Progress
        {
            get
            {
                return Aspek != null ? (Nilai * Aspek.BobotPenilaian) / 100:0;
            }
        }

    }
}


