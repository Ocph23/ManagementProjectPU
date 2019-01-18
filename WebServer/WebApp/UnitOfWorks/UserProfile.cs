using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.UnitOfWorks
{
    public class UserProfile
    {

        public konsultan  GetConsultanProfile(string userid)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Consultans.Where(O => O.UserId == userid).FirstOrDefault();
                    if (result != null)
                        return result;
                    throw new SystemException("Profile User Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }


        public pengusaha GetCompanyProfile(string userid)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Companies.Where(O => O.UserId == userid).FirstOrDefault();
                    if (result != null)
                        return result;
                    throw new SystemException("Profile User Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

    }
}