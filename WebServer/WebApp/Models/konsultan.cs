using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{ 
     [TableName("konsultan")] 
     public class konsultan
   {
          [PrimaryKey("ID")] 
          [DbColumn("ID")] 
          public int ID {  get; set;} 

          [DbColumn("Nama Perusahaan")] 
          public string Perusahaan {  get; set;} 

          [DbColumn("Alamat")] 
          public string Alamat {  get; set;} 

          [DbColumn("Pimpinan")] 
          public string Pimpinan {  get; set;} 

          [DbColumn("Email")] 
          public string Email {  get; set;} 

          [DbColumn("UserId")] 
          public string UserId {  get; set;} 

     }
}


