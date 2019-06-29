using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace MonitoringPU.Models
{ 
     public class konsultan
   {
         
          public int ID {  get; set;} 

          public string Perusahaan {  get; set;} 

          public string Alamat {  get; set;} 

          public string Pimpinan {  get; set;} 

          public string Email {  get; set;}

        public string Telepon { get; set; }

        public string UserId {  get; set;}

          public IEnumerable<project> Projects { get;  set; }
    }
}


