using System;
using Biz121.Web.ExplorerOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.BizTalk.ExplorerOM;

namespace Biz121.Tests
{
    [TestClass]
    public class SPManagerTest
    {
        [TestMethod]
        public void GetSP()
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    var rp = SPManager.GetSendPort(root, "SP_LMS_ForecastBilling_File_To_SFTP");
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    Assert.Fail("Failed to Geta Receive Port");
                   
                }
            }
          
        }
    }
}
