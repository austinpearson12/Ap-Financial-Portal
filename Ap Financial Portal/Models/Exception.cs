using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ap_Financial_Portal.Models
{
    public class Exception
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public string HelpLink { get; set; }
        public string Data { get; set; }
        public string TargetSite { get; set; }
    }
}