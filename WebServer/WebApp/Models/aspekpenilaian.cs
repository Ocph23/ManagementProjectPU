using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{ 
     [TableName("aspekpenilaian")] 
     public class aspekpenilaian   
   {
          [PrimaryKey("AspekPenialainId")] 
          [DbColumn("AspekPenialainId")] 
          public int AspekPenialainId {  get; set;} 

          [DbColumn("ProjekId")] 
          public int ProjekId {  get; set;} 

          [DbColumn("Urutan")] 
          public int Urutan {  get; set;} 

          [DbColumn("KonsultanId")] 
          public int KonsultanId {  get; set;} 

          [DbColumn("Aspek")] 
          public string Aspek {  get; set;} 

          [DbColumn("BobotPenilaian")] 
          public double BobotPenilaian {  get; set;} 

          [DbColumn("Keterangan")] 
          public string Keterangan {  get; set;} 

     }
}


