using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWProject
    {
        internal project Post(project item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    item.ProjekId = db.Projects.InsertAndGetLastID(item);
                    if (item.ProjekId <= 0)
                        throw new SystemException("Data Tidak Tersimpan !");
                    else
                        return item;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal IEnumerable<project> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Projects.Select();
                return result.ToList();
            }
        }

        internal project GetItemById(int projectId)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var item = db.Projects.Where(O => O.ProjekId == projectId).FirstOrDefault();
                    if (item == null)
                        throw new SystemException("Data Tidak Ditemukan !");
                    else
                    {
                        var datas = from a in db.AspekPenilaians.Where(O => O.ProjekId == projectId)
                                    join b in db.ItemsPenilaian.Select() on
                                    a.AspekPenialainId equals b.AspekPenialainId
                                    select new itempenilaian { AspekPenialainId=b.AspekPenialainId, DeskripsiItem=b.DeskripsiItem, ItemPenilaianId=b.ItemPenilaianId,
                                     Keterangan=b.Keterangan, Nilai=b.Nilai, Periode=b.Periode,Aspek=a};


                        item.Penilaians = datas.ToList();
                        return item;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        internal bool Delete(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var deleted = db.Projects.Delete(O => O.ProjekId == id);
                    if (deleted)
                        throw new SystemException("Data Tidak Tersimpan !");
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        internal project Put(project item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var updated = db.Projects.Update(O => new { O.KonsultanId,O.PengusahaId,O.UnitKerjaId,
                        O.Koordinat,O.LokasiPekerjaan,O.Map,O.NilaiKontrak,O.NomorKontrak,O.NamaPekerjaan,O.TanggalKontrak }, item, O =>O.ProjekId==item.ProjekId);
                    if (item.ProjekId <= 0)
                        throw new SystemException("Data Tidak Tersimpan !");
                    else
                        return item;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }
    }
}