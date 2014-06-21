using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz121.Web.Models
{
    public class BizRL
    {
        private string _transportTypeData = "<CustomProps><FileNetFailRetryInt vt=\"19\">{0}</FileNetFailRetryInt><FileNetFailRetryCount vt=\"19\">{1}</FileNetFailRetryCount></CustomProps>";
        private string _pipelineName = "Microsoft.BizTalk.DefaultPipelines.PassThruReceive";


        public string Name { get; set; }
        public string TransportType { get; set; }
        public string Address { get; set; }
       
        public int RetryCount { get; set; }
        public int RetryInterval { get; set; }

        public string PipelineName {
            get { return _pipelineName; }
            set { _pipelineName = value ; }
        }

        [JsonIgnore]
        public string TransportTypeData
        {
            get { return String.Format(_transportTypeData,RetryInterval, RetryCount);}
            set
            {
               _transportTypeData = value;
            }
        }
    }
}