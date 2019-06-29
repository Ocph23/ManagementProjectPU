﻿using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApp.Models;
using WebApp.UnitOfWorks;

namespace WebApp.Controllers
{
    public class KonsultansController : AppApiController
    {
        private UOWConsultan context = new UOWConsultan();

        // GET: api/Company
        public IEnumerable<konsultan> Get()
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
        public async Task<IHttpActionResult> Post([FromBody]konsultan item)
        {
            IdentityResult result = null;
            var userModel = new Models.ApplicationUser { Email = item.Email, UserName = item.Email };
            try
            {
                Random rand = new Random();
                var password = Helper.GetRandomAlphanumericString(6) + "3#";
                result = await UserManager.CreateAsync(userModel, password);
                if (result.Succeeded)
                {
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(userModel.Id);
                    System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);
                    string callbackUrl = urlHelper.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = userModel.Id, code = code },
                        HttpContext.Current.Request.Url.Scheme
                        );

                    await UserManager.SendEmailAsync(userModel.Id, "Confirm your account", "Your Password : " + password + " , and Please confirm your account by clicking this : <a href=\"" + callbackUrl + "\">link</a>");
                    string roleName = "Konsultan";
                    if (!await RoleManager.RoleExistsAsync(roleName))
                    {
                        var roleCreate = RoleManager.Create(new IdentityRole("2", roleName));
                        if (!roleCreate.Succeeded)
                            throw new SystemException("User Tidak Berhasil Ditambah");
                    }
                    var addUserRole = await UserManager.AddToRoleAsync(userModel.Id, roleName);

                    if (!addUserRole.Succeeded)
                    {
                        throw new SystemException("User Tidak Berhasil Ditambah");
                    }

                    item.UserId = userModel.Id;

                    var user = context.Post(item);
                    if (user != null)
                    {
                        return Ok(user);
                    }
                }
                throw new SystemException("User Tidak Berhasil Ditambah");

            }
            catch (Exception ex)
            {
                if (result != null && result.Succeeded)
                    UserManager.Delete(userModel);
                return BadRequest(ex.Message);
            }
        }




        // PUT: api/Company/5
        public IHttpActionResult Put(int id, [FromBody]konsultan value)
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
