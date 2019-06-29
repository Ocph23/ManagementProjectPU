using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWPhotos
    {
        internal async Task<IEnumerable<foto>> GetByProjectId(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var result = from a in db.Projects.Where(O=>O.ProjekId==id)
                                 join b in db.AspekPenilaians.Select() on a.ProjekId equals b.ProjekId
                                 join c in db.ItemsPenilaian.Select() on b.AspekPenialainId equals c.AspekPenialainId
                                 join d in db.Fotos.Select() on c.ItemPenilaianId equals d.ItemPenilaianId
                                 select new foto { Foto = d.Foto, Id = d.Id, ItemPenilaianId = d.ItemPenilaianId, ItemPenilaian = c};

                    return await Task.FromResult(result.ToList());
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal async Task<IEnumerable<foto>> GetByAspekId(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var result = from b in db.AspekPenilaians.Where(O=>O.AspekPenialainId==id)
                                 join c in db.ItemsPenilaian.Select() on b.AspekPenialainId equals c.AspekPenialainId
                                 join d in db.Fotos.Select() on c.ItemPenilaianId equals d.ItemPenilaianId
                                 select new foto { Foto = d.Foto, Id = d.Id, ItemPenilaianId = d.ItemPenilaianId, ItemPenilaian = c };

                    return await Task.FromResult(result.ToList());
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal Task<bool> Delete(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    if(!db.Fotos.Delete(O=>O.Id==id))
                        return Task.FromResult(true);
                    else
                        throw new SystemException("Foto Tidak Terhapus");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal Task<int> Insert(foto value)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var id = db.Fotos.InsertAndGetLastID(value);
                    if (id > 0)
                        return Task.FromResult( id);
                    else
                        throw new SystemException("Foto Tidak Tersimpan");
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        internal async Task<IEnumerable<foto>> GetByItemId(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var result = from c in db.ItemsPenilaian.Where(O=>O.ItemPenilaianId==id)
                                 join d in db.Fotos.Select() on c.ItemPenilaianId equals d.ItemPenilaianId
                                 select new foto { Foto = d.Foto, Id = d.Id, ItemPenilaianId = d.ItemPenilaianId, ItemPenilaian = c };

                    return await Task.FromResult(result.ToList());
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }
    }
}