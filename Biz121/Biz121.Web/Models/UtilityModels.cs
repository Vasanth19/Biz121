using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz121.Web.Models
{

    public class FMR
    {
        public string RPName { get; set; }
        public string SPName { get; set; }
        public BizEmail EmailInfo { get; set; }
    }

    public class BizEmail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }

        [JsonIgnore]
        public string TransportTypeData
        {
            get
            {
                return String.Format("<CustomProps>" +
                        "<From vt=\"8\">{0}</From>" +
                        "<To vt=\"8\">{1}</To>" +
                        "<Subject vt=\"8\">{2}</Subject>"+
                        "<EmailBodyText vt=\"8\">{3}</EmailBodyText>"+
                        "<SMTPHost vt=\"8\">{4}</SMTPHost>" +
                        "</CustomProps>",From,To,Subject,Body,Host);
            }
        }
    }
}