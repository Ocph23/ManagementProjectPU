using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.UnitOfWorks;

namespace WebApp.Controllers
{
    public class PenilaianController : ApiController
    {
       
        // GET: api/Penilaian
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Penilaian/5
        public string Get(int id)
        {

            return "value";
        }
                   [HttpGet]
                   [Route("api/penilaian/newperiode")]
        public IHttpActionResult GetNewPeriode(int id)
        {
            try
            {
                var context = new UnitOfWorks.UOWPenilaian(id);
                return Ok(context.GetNewPeriode());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST: api/Penilaian
        public void Post([FromBody]project value)
        {
           
        }

        // PUT: api/Penilaian/5
        public IHttpActionResult Put(int id, [FromBody]Periode value)
        {
            try
            {
                var context = new UnitOfWorks.UOWPenilaian(id);
                return Ok(context.CreateNewPenilaian(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Penilaian/5
        public void Delete(int id)
        {
        }
    }
}
