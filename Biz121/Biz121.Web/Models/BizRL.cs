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
            get
            {
                if (TransportType == "FILE")
                {
                    return String.Format(_transportTypeData, RetryInterval, RetryCount);
                }
                else
                { string customProps = "<CustomProps><ConnectionLimit vt=\"3\">5</ConnectionLimit><UserName vt=\"8\">essbase</UserName><AccessAnySSHServerHostKey vt=\"11\">0</AccessAnySSHServerHostKey><Port vt=\"3\">22</Port> " +
                                        "<ClientAuthenticationMode vt=\"8\">Password</ClientAuthenticationMode><PollingInterval vt=\"3\">0</PollingInterval><Password vt=\"1\" >123456</Password><FolderPath vt=\"8\">/folder/Xyx</FolderPath><ServerAddress vt=\"8\">devsftp</ServerAddress>" + 
                                        "<SSHServerHostKey vt=\"8\">ssh-rsa 1024 c1:fe:b2:95:68:f0:2b:40:ea:24:68:6e:e7:e9:fd:7e</SSHServerHostKey><PollingIntervalUnit vt=\"8\">Seconds</PollingIntervalUnit></CustomProps>";
                    return String.Format(customProps);
                }
          
            }
            set
            {
               _transportTypeData = value;
            }
        }

        public bool Status { get; set; }
    }
}