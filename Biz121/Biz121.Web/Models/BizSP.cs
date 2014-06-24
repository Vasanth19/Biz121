using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz121.Web.Models
{
    public class BizSP
    {
        private string _pipelineName = "Microsoft.BizTalk.DefaultPipelines.PassThruTransmit";

        public string Name { get; set; }
        public string Description { get; set; }
        public string ApplicationName { get; set; }

        public string Filter { get; set; }

        public int RetryCount { get; set; }

        public int RetryInterval { get; set; }

        public string TransportType { get; set; }

        public string Address { get; set; }

        // Used only on Post request
        public string SubscribeToRP { get; set; }

        public string PipelineName
        {
            get { return _pipelineName; }
            set { _pipelineName = value; }
        }

        [JsonIgnore]
        public string TransportTypeData
        {
            get;
            set;
        }

        public string Status { get; set; }
    }
}