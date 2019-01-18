using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWConsultan
    {
        internal konsultan Post(konsultan item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    item.ID = db.Consultans.InsertAndGetLastID(item);
                    if (item.ID <= 0)
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

        internal IEnumerable<konsultan> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Consultans.Select();
                return result.ToList();
            }
        }


        internal bool Delete(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var deleted = db.Consultans.Delete(O => O.ID == id);
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


        internal konsultan Put(konsultan item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var updated = db.Consultans.Update(O => new { O.Alamat, O.Perusahaan, O.Pimpinan}, item, O => O.ID==item.ID);
                    if (item.ID <= 0)
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