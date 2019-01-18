using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{ 
     [TableName("pengusaha")] 
     public class pengusaha 
   {
          [PrimaryKey("PengusahaId")] 
          [DbColumn("PengusahaId")] 
          public int PengusahaId {  get; set;} 

          [DbColumn("Nama")] 
          public string Nama {  get; set;} 

          [DbColumn("Direktur")] 
          public string Direktur {  get; set;} 

          [DbColumn("Alamat")] 
          public string Alamat {  get; set;} 

          [DbColumn("Email")] 
          public string Email {  get; set;} 

          [DbColumn("UserId")] 
          public string UserId {  get; set;}
        public List<project> Projects { get; internal set; }
    }
}


