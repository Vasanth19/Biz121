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
        private string _transportTypeData;
        public string TransportTypeData
        {
            get
            {
                return GetTransportTypeData();
            }
            set
            {
                _transportTypeData = value;
            }
        }

        private string GetTransportTypeData()
        {
           if (TransportType == "SFTP")
            {
                string customProps = "<CustomProps><UserName vt=\"8\">CHANGEIT</UserName><AccessAnySSHServerHostKey vt=\"11\">0</AccessAnySSHServerHostKey>" +
                "<Port vt=\"3\">22</Port><TargetFileName vt=\"8\">%SourceFileName%</TargetFileName><AppendIfExists vt=\"11\">0</AppendIfExists>" +
                "<ConnectionLimit vt=\"3\">5</ConnectionLimit><Password vt=\"1\" />" +
                "<FolderPath vt=\"8\">{0}</FolderPath><ServerAddress vt=\"8\">devsftp</ServerAddress>" +
                "<SSHServerHostKey vt=\"8\">ssh-rsa 1024 c1:fe:b2:95:68:f0:2b:40:ea:24:68:6e:e7:e9:fd:7e</SSHServerHostKey><ClientAuthenticationMode vt=\"8\">Password</ClientAuthenticationMode></CustomProps>";
                string fileMask = Address.Substring(Address.LastIndexOf("/", StringComparison.InvariantCulture) + 1);
                string folderPath = Address.Substring(17).Replace(fileMask, String.Empty);
                return String.Format(customProps, folderPath);
            }
            else
            {
                return _transportTypeData;
            }

        }

        public string Status { get; set; }
    }
}