using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz121.Web.Models
{
    public class BizRL
    {
        private string _transportTypeData;
        private string _pipelineName = "Microsoft.BizTalk.DefaultPipelines.PassThruReceive";


        public string Name { get; set; }
        public string TransportType { get; set; }
        public string Address { get; set; }

        public string PipelineName {
            get { return _pipelineName; }
            set { _pipelineName = value ; }
        }

       // [JsonIgnore]
        public string TransportTypeData
        {
            get
            {
                return _transportTypeData;//GetTransportTypeData();
            }
            set
            {
               _transportTypeData = value;
            }
        }

        private string GetTransportTypeData()
        {
            if (TransportType == "FILE")
            {
                _transportTypeData = "<CustomProps><FileNetFailRetryInt vt=\"19\">{0}</FileNetFailRetryInt><FileNetFailRetryCount vt=\"19\">{1}</FileNetFailRetryCount></CustomProps>";
                return String.Format(_transportTypeData);
            }
            else if (TransportType == "SFTP")
            {
                string customProps =
                    "<CustomProps><ConnectionLimit vt=\"3\">5</ConnectionLimit><UserName vt=\"8\">ChangeIt</UserName>" +
                    "<AccessAnySSHServerHostKey vt=\"11\">0</AccessAnySSHServerHostKey><Port vt=\"3\">22</Port> " +
                    "<ClientAuthenticationMode vt=\"8\">Password</ClientAuthenticationMode><PollingInterval vt=\"3\">5</PollingInterval><Password vt=\"1\" >Replaceit</Password>" +
                    "<FolderPath vt=\"8\">{0}</FolderPath><FileMask vt=\"8\">{1}</FileMask>" +
                    "<ServerAddress vt=\"8\">devsftp</ServerAddress>" +
                    "<SSHServerHostKey vt=\"8\">ssh-rsa 1024 c1:fe:b2:95:68:f0:2b:40:ea:24:68:6e:e7:e9:fd:7e</SSHServerHostKey>" +
                    "<PollingIntervalUnit vt=\"8\">Minutes</PollingIntervalUnit></CustomProps>";
         
                string fileMask = Address.Substring(Address.LastIndexOf("/",StringComparison.InvariantCulture)+1);
                string folderPath = Address.Substring(17).Replace(fileMask,String.Empty);
                return String.Format(customProps,folderPath,fileMask);
            }
            else
            {
                return string.Empty;
            }
        }

        public bool Status { get; set; }
    }
}