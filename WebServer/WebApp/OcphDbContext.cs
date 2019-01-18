using Ocph.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp
{
    public class OcphDbContext : Ocph.DAL.Provider.MySql.MySqlDbConnection
    {
        private string connectionStringName = "DefaultConnection";
        public OcphDbContext()
        {
           this.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        public IRepository<konsultan> Consultans { get { return new Repository<konsultan>(this); } }
        public IRepository<pengusaha> Companies { get { return new Repository<pengusaha>(this); } }
        public IRepository<project> Projects { get { return new Repository<project>(this); } }
        public IRepository<aspekpenilaian> AspekPenilaians { get { return new Repository<aspekpenilaian>(this); } }
        public IRepository<itempenilaian> ItemsPenilaian { get { return new Repository<itempenilaian>(this); } }
        public IRepository<foto> Fotos { get { return new Repository<foto>(this); } }
        public IRepository<unitkerja>UnitKerjas{ get { return new Repository<unitkerja>(this); } }

    }
}