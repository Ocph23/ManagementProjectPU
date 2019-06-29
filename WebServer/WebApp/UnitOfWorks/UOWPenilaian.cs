using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UOWPenilaian
    {
        private readonly int ProjectId;
        private UOWProject uowProject = new UOWProject();
        public UOWPenilaian(int projectId)
        {
            this.ProjectId = projectId;
            Load();
        }

        public project Project { get; private set; }

        private void Load()
        {
            try
            {
                
                this.Project = uowProject.GetItemById(ProjectId);
                Normalization();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public List<Periode> GetDataNilai()
        {
            var groups = this.Project.Penilaians.GroupBy(O => O.Periode);
            List<Periode> list = new List<Periode>();
            foreach(var item in groups)
            {
               var res = item.Select(O => O);
                var data = new Periode { PeriodeId = item.Key, Data = res.ToList() };
                list.Add(data);
            }
            return list;
        }

        public Periode GetNewPeriode()
        {
            int periode = 1;
            var data=this.Project.Penilaians.OrderBy(O => O.Periode).LastOrDefault();
            if (data != null)
                periode = data.Periode + 1;
            return new Periode { PeriodeId = periode };
        }

        public Periode CreateNewPenilaian(Periode periode)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    foreach (var item in Project.AspekPenilaian)
                    {
                        var datas = periode.Data.Where(O => O.AspekPenialainId == item.AspekPenialainId && O.Periode==periode.PeriodeId);
                        if(datas.Count()>0)
                        {
                            foreach (var dataitem in datas)
                            {
                                if(dataitem.ItemPenilaianId<=0)
                                {
                                    dataitem.ItemPenilaianId = db.ItemsPenilaian.InsertAndGetLastID(dataitem);
                                    if (dataitem.ItemPenilaianId <= 0)
                                        throw new SystemException("Data Tidak Tersimpan !");
                                }
                                else
                                {
                                    if(!db.ItemsPenilaian.Update(O=>new {O.Keterangan,O.Nilai},dataitem,
                                        O=>O.ItemPenilaianId==dataitem.ItemPenilaianId))
                                        throw new SystemException("Data Tidak Tersimpan !");
                                }
                               
                            }
                        }
                        else
                        {
                            var data = new itempenilaian
                            {
                                Periode = periode.PeriodeId,
                                AspekPenialainId = item.AspekPenialainId,
                                Nilai = 0,
                                Keterangan = ""
                            };

                            data.ItemPenilaianId = db.ItemsPenilaian.InsertAndGetLastID(data);
                            if (data.ItemPenilaianId > 0)
                                periode.Data.Add(data);
                            else
                                throw new SystemException("Data Tidak Tersimpan !");
                        }
                       
                    }

                    trans.Commit();
                    return periode;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
        }


        private void Normalization()
        {
            try
            {
                var periodes = GetDataNilai();
                foreach (var aspek in Project.AspekPenilaian)
                {
                    foreach (var periode in periodes)
                    {
                        foreach (var item in periode.Data)
                        {
                            if (!ItemIsExist(aspek.AspekPenialainId, periode.PeriodeId))
                            {
                                using (var db = new OcphDbContext())
                                {
                                    var itemData = new itempenilaian
                                    {
                                        AspekPenialainId = aspek.AspekPenialainId,
                                        Periode = periode.PeriodeId
                                    };

                                    itemData.ItemPenilaianId = db.ItemsPenilaian.InsertAndGetLastID(itemData);
                                    if (itemData.ItemPenilaianId > 0)
                                        periode.Data.Add(itemData);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception )
            {

                
            }
        }

        private bool ItemIsExist(int aspekPenialainId, int periodeId)
        {
            var result = Project.Penilaians.Where(O => O.AspekPenialainId == aspekPenialainId && O.Periode == periodeId).FirstOrDefault();
            if (result == null)
                return false;
            return true;
        }
             

    }

    
}