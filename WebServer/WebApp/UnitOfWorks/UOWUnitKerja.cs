using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWUnitKerja
    {
        internal unitkerja Post(unitkerja item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    item.UnitKerjaId = db.UnitKerjas.InsertAndGetLastID(item);
                    if (item.UnitKerjaId <= 0)
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

        internal object GetItemById(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var item = db.UnitKerjas.Where(O => O.UnitKerjaId == id).FirstOrDefault();
                    if (item == null)
                        throw new SystemException("Data Tidak Ditemukan !");
                    else
                    {
                       
                        return item;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal IEnumerable<unitkerja> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.UnitKerjas.Select();
                return result.ToList();
            }
        }


        internal bool Delete(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var deleted = db.UnitKerjas.Delete(O => O.UnitKerjaId == id);
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


        internal unitkerja Put(unitkerja  item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var updated = db.UnitKerjas.Update(O => new { O.Keterangan, O.Nama, O.Pimpinan }, item, O => O.UnitKerjaId == item.UnitKerjaId);
                    if (item.UnitKerjaId<= 0)
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