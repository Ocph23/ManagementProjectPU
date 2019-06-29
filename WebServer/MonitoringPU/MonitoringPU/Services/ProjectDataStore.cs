using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MonitoringPU.Models;

namespace MonitoringPU.Services
{
    public class ProjectDataStore : IDataStore<project>
    {
        List<project> items;

      
        public async Task<bool> AddItemAsync(project item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(project item)
        {
            var oldItem = items.Where((project arg) => arg.ProjekId == item.ProjekId).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var projekid = Convert.ToInt32(id);
            var oldItem = items.Where((project arg) => arg.ProjekId == projekid).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<project> GetItemAsync(string id)
        {
            var projekid = Convert.ToInt32(id);
            return await Task.FromResult(items.FirstOrDefault(s => s.ProjekId == projekid));
        }

        public async Task<IEnumerable<project>> GetItemsAsync(bool forceRefresh = false)
        {
            //return new List<project>() {
            //        new project{
            //            AspekPenilaian =new List<aspekpenilaian>()
            //            {
            //                new aspekpenilaian{ Aspek="Aspek I", BobotPenilaian=20, ProjekId=1, Keterangan="Apakek", AspekPenialainId=1, Urutan=1 }
            //            } ,
            //            Bidang = new unitkerja{ Nama="Bidang I", UnitKerjaId=1, Pimpinan="Pimpinan Bidang" },
            //             Konsultan= new konsultan{ Perusahaan="PT. ", Pimpinan="Direktri", ID=1,  },

            //            Kontraktor= new pengusaha{ Nama="CV.", Direktur="Pimpinan !", PengusahaId=1 },
            //            LastPeriode= new Periode{ PeriodeId=1, Data=new List<itempenilaian>{ new itempenilaian { AspekPenialainId=1, Nilai=43, Periode=1, ItemPenilaianId=1, Aspek=
            //              new aspekpenilaian{ Aspek="Aspek I", BobotPenilaian=20, ProjekId=1, Keterangan="Apakek", AspekPenialainId=1, Urutan=1 }} } }   ,
            //             Periodes=new ObservableCollection<Periode>
            //             {
            //                   new Periode{ PeriodeId=1, Data=new List<itempenilaian>{ new itempenilaian { AspekPenialainId=1, Nilai=43, Periode=1, ItemPenilaianId=1, Aspek=
            //              new aspekpenilaian{ Aspek="Aspek I", BobotPenilaian=20, ProjekId=1, Keterangan="Apakek", AspekPenialainId=1, Urutan=1 }} } }   ,

            //             } ,

            //              KonsultanId=1,    Koordinat="1222,21212", LamaPekerjaan=230, LokasiPekerjaan="jln.", NilaiKontrak=125225, ProjekId=1, UnitKerjaId=1, TanggalKontrak=DateTime.Now,
            //            NamaPekerjaan="Proyek A", NomorKontrak="12233",      PengusahaId     =1,


            //        }
            //        };
            try
            {
                using (var res = new RestServices())
                {
                    IEnumerable<project> result = await res.Get<List<project>>("api/projects");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return null;
            }
        }

        public async Task<Periode> SavePenilaian(Periode periodeSelected,int id)
        {
            try
            {
                using (var res = new RestServices())
                {
                    var result = await res.Put<Periode>("api/penilaian/"+id.ToString(), periodeSelected);
                    if (result != null)
                        return result;
                    else
                        throw new SystemException("Data Tidak Tersimpan");
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return null;
            }
        }

        public async Task AddNewFoto(foto data)
        {
            try
            {
                using (var res = new RestServices())
                {
                    var result = await res.PostAsync("api/fotos", Helper.Content(data));
                    if (result == null)
                        throw new SystemException("Data Tidak Tersimpan");
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
            }
        }
    }
}