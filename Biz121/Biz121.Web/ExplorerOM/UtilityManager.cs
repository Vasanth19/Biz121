using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz121.Web.Utilities;
using Biz121.Web.Models;
using System.Configuration;


namespace Biz121.Web.ExplorerOM
{
    public class UtilityManager
    {
        public static void CreateSMTPSendPort(BtsCatalogExplorer root, FMR fmr)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            Application App = root.ReceivePorts[fmr.RPName].Application;
            if (App == null)
            {
                App = root.Applications["BizTalk Application 1"]; // Hardcoded default
            }
            SendPort sendPort = App.AddNewSendPort(false, false);

            sendPort.Name = "Error_Email_For_" + fmr.SPName + "_And_" + fmr.RPName;
            sendPort.Description = "CBR Fail Message Routing handle for Receive Port" + fmr.SPName + " Send port" + fmr.SPName;

            sendPort.PrimaryTransport.TransportType = root.ProtocolTypes["SMTP"];
            sendPort.PrimaryTransport.Address = fmr.EmailInfo.To;
            sendPort.SendPipeline = root.Pipelines["Microsoft.BizTalk.DefaultPipelines.PassThruTransmit"];
            sendPort.Filter = fmr.RPName.Filters_AddFailedMsg(fmr.SPName);

            sendPort.PrimaryTransport.TransportTypeData = fmr.EmailInfo.TransportTypeData;
            
            sendPort.Status = PortStatus.Started;
            root.SaveChanges();
        }

     
    }
}