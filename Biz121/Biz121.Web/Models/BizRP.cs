using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz121.Web.Models
{
    public class BizRP
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RLCount { get; set; }
        public string PrimaryRL { get; set; }
        public List<BizRL> RLs { get; set; }
        public string ApplicationName { get; set; }
    }
}