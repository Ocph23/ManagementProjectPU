using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApp.Models;
using WebApp.UnitOfWorks;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
    public class ProjectsController : ApiController
    {
        // GET: api/Projects
        private UOWProject context = new UOWProject();

        // GET: api/Company
        
        public IHttpActionResult Get()
        {
            try
            {
                var result = context.Get();
                 if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    if(User.IsInRole("Konsultan"))
                    {
                        var konsultan = new UOWConsultan().GetByUserId(userId);
                        if (konsultan != null)
                            return Ok(result.Where(O => O.KonsultanId == konsultan.ID));
                        else
                            throw new SystemException("Anda Tidak Memiliki Akses");
                    }
                    else  if(User.IsInRole("Kontraktor"))
                    {
                        var pengusaha = new UOWPengusaha().GetByUserId(userId);
                        if (pengusaha != null)
                            return Ok(result.Where(O => O.KonsultanId == pengusaha.PengusahaId));
                        else
                            throw new SystemException("Anda Tidak Memiliki Akses");
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [Authorize]
        public IHttpActionResult Post([FromBody]project item)
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
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody]project value)
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
        [Authorize]
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
