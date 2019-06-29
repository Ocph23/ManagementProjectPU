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

        internal IEnumerable<aspekpenilaian> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.AspekPenilaians.Select();
                return result.ToList();
            }
        }


        internal bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var deleted = db.AspekPenilaians.Delete(O => O.AspekPenialainId == id);
                    if (deleted)
                        throw new SystemException("Data Tidak Tersimpan !");
                    else
                    {
                        if (!db.ItemsPenilaian.Delete(O => O.AspekPenialainId == id))
                        {
                            throw new SystemException("Data Tidak Tersimpan !");
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
               

            }
            
        }


        internal aspekpenilaian Put(aspekpenilaian item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var updated = db.AspekPenilaians.Update(O => new {
                        O.KonsultanId, O.BobotPenilaian,O.Keterangan,O.Urutan
                    }, item, O => O.AspekPenialainId == item.AspekPenialainId);
                    if (updated)
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