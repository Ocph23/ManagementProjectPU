using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{
    [TableName("projects")]
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
                return Convert.ToDouble(Koordinat.Split(';')[0]);
            } }

        public double Latitude
        {
            get
            {
                return Convert.ToDouble(Koordinat.Split(';')[1]);
            }
        }

        public List<itempenilaian> Penilaians { get; set; }

    }
}


