using System;
using Biz121.Web.ExplorerOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.BizTalk.ExplorerOM;

namespace Biz121.Tests
{
    [TestClass]
    public class RPManagerTest
    {
        [TestMethod]
        public void GetRP()
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    var rp = RPManager.GetReceivePort(root,"RP_ESSBase");
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    Assert.Fail("Failed to Geta Receive Port");
                   
                }
            }
          
        }
    }
}
