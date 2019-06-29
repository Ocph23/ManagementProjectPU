using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringPU.Models
{
    public class project
    {
       
        public int ProjekId { get; set; }

        public int PengusahaId { get; set; }

        public int UnitKerjaId { get; set; }

        public int KonsultanId { get; set; }

        public string NomorKontrak { get; set; }

        public string NamaPekerjaan { get; set; }

        public string LokasiPekerjaan { get; set; }

        public string Koordinat { get; set; }

        public double NilaiKontrak { get; set; }

        public int LamaPekerjaan { get; set; }

        public DateTime TanggalKontrak { get; set; }

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

        public List<itempenilaian> Penilaians { get; set; }
        public IEnumerable<aspekpenilaian> AspekPenilaian { get;  set; }
        public ObservableCollection<Periode> Periodes { get;  set; }
        public double Progress { get;  set; }
        public konsultan Konsultan { get; set; }
        public pengusaha Kontraktor { get; set; }
        public unitkerja Bidang { get; set; }
        public Periode LastPeriode { get; set; }

        public ICommand PenilaianCommand { get; set; }
        public ICommand ProgressCommand { get; set; }
        public ICommand LocationCommand { get; set; }



    }
}


