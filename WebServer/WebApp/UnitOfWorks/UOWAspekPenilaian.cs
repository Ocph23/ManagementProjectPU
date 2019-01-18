using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWAspekPenilaian
    {
        internal aspekpenilaian Post(aspekpenilaian item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    item.ProjekId = db.AspekPenilaians.InsertAndGetLastID(item);
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
                    var updated = db.Projects.Update(O => new {
                        O.KonsultanId,
                        O.PengusahaId,
                        O.UnitKerjaId,
                        O.Koordinat,
                        O.LokasiPekerjaan,
                        O.Map,
                        O.NilaiKontrak,
                        O.NomorKontrak,
                        O.NamaPekerjaan,
                        O.TanggalKontrak
                    }, item, O => O.ProjekId == item.ProjekId);
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