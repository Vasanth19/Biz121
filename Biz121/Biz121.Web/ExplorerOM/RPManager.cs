using Biz121.Web.Models;
using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Biz121.Web.ExplorerOM
{
    public static class RPManager
    {
        #region Get Methods
        public static List<BizRP> GetReceivePorts(BtsCatalogExplorer root)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

            List<BizRP> listPorts = new List<BizRP>();
            foreach (ReceivePort rp in root.ReceivePorts)
            {
                List<BizRL> listlocations = new List<BizRL>();
                foreach (ReceiveLocation r in rp.ReceiveLocations)
                {
                    BizRL rl = MapToBizRL(r);
                    listlocations.Add(rl);

                }
                BizRP port = MapToBizRP(rp, listlocations);
                listPorts.Add(port);
            }
            return listPorts;

        }
        public static BizRP GetReceivePort(BtsCatalogExplorer root, string rpName)
        {
             root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
                ReceivePort rp = root.ReceivePorts[rpName];

                    if (rp == null)
                    {
                        return null;
                    }

                    List<BizRL> listlocations = new List<BizRL>();
                    foreach (ReceiveLocation r in rp.ReceiveLocations)
                    {
                        BizRL rl = MapToBizRL(r);
                        listlocations.Add(rl);
                    }
                    BizRP port = MapToBizRP(rp, listlocations);
                    return port;
        }

        private static BizRP MapToBizRP(ReceivePort rp, List<BizRL> listlocations)
        {
            BizRP port = new BizRP()
            {
                Name = rp.Name,
                Description = rp.Description,
                RLCount = rp.ReceiveLocations.Count,
                PrimaryRL = rp.PrimaryReceiveLocation.Name,
                RLs = listlocations,
                ApplicationName = rp.Application.Name
            };
            return port;
        }
        private static BizRL MapToBizRL(ReceiveLocation r)
        {
            BizRL rl = new BizRL()
            {
                Name = r.Name,
                TransportType = r.TransportType.Name,
                Address = r.PublicAddress,
                PipelineName = r.ReceivePipeline.FullName,
                TransportTypeData = r.TransportTypeData
            };
            return rl;
        }
        #endregion

        #region Create Methods

        public static bool CreateReceivePort(BtsCatalogExplorer root, BizRP port)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            Application App = root.Applications[port.ApplicationName.Trim()];
            if (App == null)
            {
                App = root.Applications["BizTalk Application 1"]; // Hardcoded default
            }

            ReceivePort receivePort = App.AddNewReceivePort(false);

            //Note that if you dont set the name property for the receieve port, 
            //it will create a new receive location and add it to the receive       //port.
            receivePort.Name = port.Name;
            receivePort.Description = port.Description;


            foreach (BizRL rl in port.RLs)
            {
                CreatedReceiveLocation(rl, receivePort, root);
            }

            //Try to commit the changes made so far. If the commit fails, 
            //roll-back all changes.
            root.SaveChanges();
            return true;
        }
        private static void CreatedReceiveLocation(BizRL rl, ReceivePort receivePort, BtsCatalogExplorer root)
        {
            //Create a new receive location and add it to the receive port
            ReceiveLocation btsRL = receivePort.AddNewReceiveLocation();
            foreach (ReceiveHandler handler in root.ReceiveHandlers)
            {
                if (handler.TransportType.Name.ToUpper() == rl.TransportType.ToUpper())
                {
                    btsRL.ReceiveHandler = handler;
                    break;
                }
            }

            btsRL.Name = rl.Name;

            //Associate a transport protocol and URI with the receive location.
            btsRL.TransportType = root.ProtocolTypes[rl.TransportType];
            btsRL.TransportTypeData = rl.TransportTypeData;
            btsRL.Address = rl.Address;

            //Assign the first receive pipeline found to process the message.
            btsRL.ReceivePipeline = root.Pipelines["Microsoft.BizTalk.DefaultPipelines.PassThruReceive"]; //default Pipeline

            //Enable the receive location.
            btsRL.Enable = true;
            btsRL.FragmentMessages = Fragmentation.Yes;//optional property
            btsRL.ServiceWindowEnabled = false; //optional property

        }

        #endregion


        public static void DeleteReceivePort(BtsCatalogExplorer root, string rpName)
        {
            root.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

            ReceivePort receivePort = root.ReceivePorts[rpName];
            root.RemoveReceivePort(receivePort);
            //Try to commit the changes made so far. 
            root.SaveChanges();
        }
    }
}