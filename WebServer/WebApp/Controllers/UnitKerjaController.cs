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
    public class UnitKerjaController : ApiController
    {
        // GET: api/Projects
        private UOWUnitKerja context = new UOWUnitKerja();

        // GET: api/Company
        public IEnumerable<unitkerja> Get()
        {
            return context.Get();
        }

        // GET: api/Company/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(context.GetItemById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Company
        public IHttpActionResult Post([FromBody]unitkerja item)
        {
            try
            {
                return Ok(context.Post(item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // PUT: api/Company/5
        public IHttpActionResult Put(int id, [FromBody]unitkerja value)
        {
            try
            {
                return Ok(context.Put(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Company/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
