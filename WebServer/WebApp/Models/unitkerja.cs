using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{ 
     [TableName("unitkerja")] 
     public class unitkerja 
   {
          [PrimaryKey("UnitKerjaId")] 
          [DbColumn("UnitKerjaId")] 
          public int UnitKerjaId {  get; set;} 

          [DbColumn("Nama")] 
          public string Nama {  get; set;} 

          [DbColumn("Pimpinan")] 
          public string Pimpinan {  get; set;} 

          [DbColumn("Keterangan")] 
          public string Keterangan {  get; set;} 

     }
}


