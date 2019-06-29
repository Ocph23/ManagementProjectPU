using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FotosController : ApiController
    {
        UnitOfWorks.UOWPhotos context = new UnitOfWorks.UOWPhotos();
        // GET: api/Fotos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Fotos/5
        [HttpGet]
        [Route("ap/fotos/byproject")]
        public async Task<IHttpActionResult> GetbyProjectAsync(int id)
        {
            IEnumerable<foto>fotos = await context.GetByProjectId(id);
            return Ok(fotos);
        }


        [HttpGet]
        [Route("ap/fotos/byaspek")]
        public async Task<IHttpActionResult> GetByAspekId(int id)
        {
            IEnumerable<foto> fotos = await context.GetByAspekId(id);
            return Ok(fotos);
        }

        [HttpGet]
        [Route("ap/fotos/byitem")]
        public async Task<IHttpActionResult> GetByItemId(int id)
        {
            IEnumerable<foto> fotos = await context.GetByItemId(id);
            return Ok(fotos);
        }
       

        // POST: api/Fotos
        [Authorize(Roles ="Konsultan")]
        public async Task<IHttpActionResult> Post([FromBody]foto value)
        {
            try
            {
                int data = await context.Insert(value);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Fotos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Fotos/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                bool data = await context.Delete(id);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
