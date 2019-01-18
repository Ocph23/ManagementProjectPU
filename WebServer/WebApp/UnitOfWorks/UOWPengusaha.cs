using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    internal class UOWPengusaha
    {
        internal pengusaha Post(pengusaha item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                   item.PengusahaId = db.Companies.InsertAndGetLastID(item);
                    if (item.PengusahaId <= 0)
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

        internal pengusaha GetItemById(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var item = db.Companies.Where(O => O.PengusahaId == id).FirstOrDefault();
                    if (item==null)
                        throw new SystemException("Data Tidak Ditemukan !");
                    else
                    {
                        item.Projects = db.Projects.Where(O => O.PengusahaId == id).ToList();
                        return item;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal IEnumerable<pengusaha> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Companies.Select();
                return result.ToList();
            }
        }


        internal bool Delete(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var deleted = db.Companies.Delete(O=>O.PengusahaId==id);
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


        internal pengusaha Put(pengusaha item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                   var updated= db.Companies.Update(O => new { O.Alamat, O.Direktur, O.Nama }, item, O => O.PengusahaId == item.PengusahaId);
                    if (item.PengusahaId <= 0)
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