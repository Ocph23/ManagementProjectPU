using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
using WebApp.UnitOfWorks;

namespace WebApp.Models 
{

    [TableName("Projects")]
    public class project
    {
        [PrimaryKey("ProjekId")]
        [DbColumn("ProjekId")]
        public int ProjekId { get; set; }

        [DbColumn("PengusahaId")]
        public int PengusahaId { get; set; }

        [DbColumn("UnitKerjaId")]
        public int UnitKerjaId { get; set; }

        [DbColumn("KonsultanId")]
        public int KonsultanId { get; set; }

        [DbColumn("NomorKontrak")]
        public string NomorKontrak { get; set; }

        [DbColumn("PaketPekerjaan")]
        public string NamaPekerjaan { get; set; }

        [DbColumn("LokasiPekerjaan")]
        public string LokasiPekerjaan { get; set; }

        [DbColumn("Koordinat")]
        public string Koordinat { get; set; }

        [DbColumn("NilaiKontrak")]
        public double NilaiKontrak { get; set; }

        [DbColumn("LamaPekerjaan")]
        public int LamaPekerjaan { get; set; }

        [DbColumn("TanggalKontrak")]
        public DateTime TanggalKontrak { get; set; }

        [DbColumn("Map")]
        public string Map { get; set; }

        public DateTime TanggalBerakhir
        {
            get
            {
                return TanggalKontrak.AddDays(LamaPekerjaan);
            }
        }

        public double Longitude { get {
                if (!string.IsNullOrEmpty(Koordinat))
                    return Convert.ToDouble(Koordinat.Split(',')[0]);
                else
                    return 0;
            } }

        public double Latitude
        {
            get
            {
                if (!string.IsNullOrEmpty(Koordinat))
                    return Convert.ToDouble(Koordinat.Split(',')[1]);
                else
                    return 0;
            }
        }

        public IEnumerable<itempenilaian> Penilaians { get; set; }
        public IEnumerable<aspekpenilaian> AspekPenilaian { get;  set; }
        public List<Periode> Periodes { get;  set; }
        public double Progress { get;  set; }
        public konsultan Konsultan { get;  set; }
        public pengusaha Kontraktor { get;  set; }
        public unitkerja Bidang { get; set; }
        public Periode LastPeriode { get;  set; }
    }
}


