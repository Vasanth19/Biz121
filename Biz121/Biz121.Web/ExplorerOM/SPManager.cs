using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Biz121.Web.Utilities;
using Biz121.Web.Models;

namespace Biz121.Web.ExplorerOM
{
    public class SPManager
    {

        #region Get Methods

        internal static List<BizSP> GetSendPorts(BtsCatalogExplorer root)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

            List<BizSP> listPorts = new List<BizSP>();
            foreach (SendPort sp in root.SendPorts)
            {
                if (!sp.IsDynamic)
                {
                    BizSP port = MapToBizSP(sp);
                    listPorts.Add(port);
                }
            }
            return listPorts;
        }
        internal static BizSP GetSendPort(BtsCatalogExplorer root, string spName)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            SendPort sp = root.SendPorts[spName];

            if (sp == null)
            {
                return null;
            }

            return MapToBizSP(sp);
        }
        private static BizSP MapToBizSP(SendPort sp)
        {
            BizSP port = new BizSP()
            {
                Name = sp.Name,
                Description = sp.Description,
                Filter = sp.Filter,
                RetryCount = sp.PrimaryTransport.RetryCount,
                RetryInterval = sp.PrimaryTransport.RetryInterval,
                TransportType = sp.PrimaryTransport.TransportType.Name,
                Address = sp.PrimaryTransport.Address,
                PipelineName = sp.SendPipeline.FullName,
                ApplicationName = sp.Application.Name,
                TransportTypeData = sp.PrimaryTransport.TransportTypeData,
                Status = sp.Status.ToString()

            };
            return port;
        }
        #endregion

        #region Get Methods
        internal static void CreateSendPort(Microsoft.BizTalk.ExplorerOM.BtsCatalogExplorer root, Models.BizSP port)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            Application App = root.Applications[port.ApplicationName.Trim()];
            if (App == null)
            {
                App = root.Applications["BizTalk Application 1"]; // Hardcoded default
            }

            SendPort sendPort = App.AddNewSendPort(false, false);

            sendPort.Name = port.Name;
            sendPort.Description = port.Description;

            sendPort.PrimaryTransport.TransportType = root.ProtocolTypes[port.TransportType];
            sendPort.PrimaryTransport.TransportTypeData = port.TransportTypeData;
            sendPort.PrimaryTransport.RetryInterval = port.RetryInterval;
            sendPort.PrimaryTransport.RetryCount = port.RetryCount;
            sendPort.PrimaryTransport.Address = port.Address;
            sendPort.SendPipeline = root.Pipelines[port.PipelineName];
            sendPort.RouteFailedMessage = true;
            sendPort.Filter = port.SubscribeToRP.Filters_AddRP();

            sendPort.Status = PortStatus.Started;
            root.SaveChanges();

            if (sendPort.RouteFailedMessage)
            {
                //Create a SMTP SendPort
             //   CreateSMTPSendPort(root,App, port.Name, port.SubscribeToRP);
            }

        }

     

    
        #endregion

        internal static void DeleteSendPort(BtsCatalogExplorer root, string spName)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

            SendPort sendPort = root.SendPorts[spName];
            sendPort.Status = PortStatus.Bound;
            root.SaveChanges();

            root.RemoveSendPort(sendPort);

            //Try to commit the changes made so far. 
            root.SaveChanges();
        }
    }
}