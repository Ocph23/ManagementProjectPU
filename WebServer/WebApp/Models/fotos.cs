using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace WebApp.Models 
{ 
     [TableName("fotos")] 
     public class foto
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id {  get; set;} 

          [DbColumn("Foto")] 
          public byte[] Foto {  get; set;} 

          [DbColumn("ItemPenilaianId")] 
          public int ItemPenilaianId {  get; set;} 

     }
}


