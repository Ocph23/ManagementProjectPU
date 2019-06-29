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
                var result = (from a in db.Projects.Select()
                              join b in db.Consultans.Select() on a.KonsultanId equals b.ID
                              join c in db.Companies.Select() on a.PengusahaId equals c.PengusahaId
                              join d in db.UnitKerjas.Select() on a.UnitKerjaId equals d.UnitKerjaId
                              select new project
                              {
                                  KonsultanId = a.KonsultanId,
                                  Koordinat = a.Koordinat,
                                  LamaPekerjaan = a.LamaPekerjaan,
                                  LokasiPekerjaan = a.LokasiPekerjaan,
                                  Map = a.Map,
                                  NamaPekerjaan = a.NamaPekerjaan,
                                  NilaiKontrak = a.NilaiKontrak,
                                  NomorKontrak = a.NomorKontrak,
                                  PengusahaId = a.PengusahaId,
                                  ProjekId = a.ProjekId,
                                  TanggalKontrak = a.TanggalKontrak,
                                  UnitKerjaId = a.UnitKerjaId, Konsultan=b, Kontraktor=c,  Bidang=d
                              }).ToList();
                           
                      foreach(var item in result)
                {

                    UOWPenilaian context = new UOWPenilaian(item.ProjekId);
                                                                  
                    item.Periodes = context.GetDataNilai().OrderByDescending(O=>O.PeriodeId).ToList();

                    var last = item.Periodes.FirstOrDefault();
                    if(last!=null)
                    {
                        item.Progress = last.Data.Sum(O => O.Progress);
                        item.LastPeriode = last;
                    }

                    item.AspekPenilaian = db.AspekPenilaians.Where(O => O.ProjekId == item.ProjekId).ToList();
                    
                }
                return result;
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
                        var aspeks = from a in db.AspekPenilaians.Where(O => O.ProjekId == projectId)
                                     select a;
                        var datas = from a in aspeks
                                    join b in db.ItemsPenilaian.Select() on
                                    a.AspekPenialainId equals b.AspekPenialainId
                                    select new itempenilaian { AspekPenialainId=b.AspekPenialainId,ItemPenilaianId=b.ItemPenilaianId,
                                     Keterangan=b.Keterangan, Nilai=b.Nilai, Periode=b.Periode,Aspek=a};

                        item.AspekPenilaian = aspeks.ToList();
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
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
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

                    if (item.AspekPenilaian != null &&item.AspekPenilaian.Count()>0 && item.AspekPenilaian.Sum(O => O.BobotPenilaian) != 100)
                    {
                        throw new SystemException("Total Aspek Penilaian Harus 100");
                    }else if (item.AspekPenilaian != null && item.AspekPenilaian.Count() > 0)
                    {
                        foreach (var data in item.AspekPenilaian)
                        {
                            if (data.AspekPenialainId <= 0)
                            {
                                data.AspekPenialainId = db.AspekPenilaians.InsertAndGetLastID(data);
                                if (data.AspekPenialainId < -0)
                                    throw new SystemException("Data Tidak Tersimpan !");
                            }
                            else
                            {
                                if (!db.AspekPenilaians.Update(O => new { O.Aspek, O.BobotPenilaian, O.Keterangan, O.KonsultanId, O.Urutan },
                                    data, O => O.AspekPenialainId == data.AspekPenialainId))
                                    throw new SystemException("Data Tidak Tersimpan !");
                            }
                        }



                        var dataInDb = db.AspekPenilaians.Where(O => O.ProjekId == item.ProjekId).ToList();
                        foreach (var dataDb in dataInDb)
                        {
                            var result = item.AspekPenilaian.Where(O => O.AspekPenialainId == dataDb.AspekPenialainId).FirstOrDefault();
                            if (result == null)
                            {
                                if (!db.AspekPenilaians.Delete(O => O.AspekPenialainId == dataDb.AspekPenialainId))
                                    throw new SystemException("Data Tidak Tersimpan !");
                            }
                        }


                    }

                    //updated

                    //deleted

                    trans.Commit();
                    return item;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
                
            }



        }
    }
}