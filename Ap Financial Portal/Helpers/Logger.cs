using Ap_Financial_Portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exception = System.Exception;

namespace Ap_Financial_Portal.Helpers
{
    public class Logger
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static void AddException(Exception ex)
        {
            var exception = new Models.Exception
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                InnerException = JsonConvert.SerializeObject(ex.InnerException, Formatting.Indented),
                HelpLink = ex.HelpLink,
                Data = JsonConvert.SerializeObject(ex.Data, Formatting.Indented),
                TargetSite = ex.TargetSite.Name
            };
            db.Exceptions.Add(exception);
            db.SaveChanges();

        }
        public static void LogInfo(string info)
        {
            var newException = new Models.Exception
            {
                Message = info
            };
            db.Exceptions.Add(newException);

            db.SaveChanges();
        }
    }
}