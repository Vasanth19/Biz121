using Biz121.Web.ExplorerOM;
using Biz121.Web.Models;
using Microsoft.BizTalk.ExplorerOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace Biz121.Web.Api
{
    [RoutePrefix("api/v1")]
    [Route("util")]
    public class UtilityController : ApiController
    {
        public IHttpActionResult Get()
        {
           
                try
                {
                    //UtilityManager.CreateSMTPSendPort(root)
                    return Ok("Hello");
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
           
        }

       [Route("util/cbr")]
       [HttpPost]
        public IHttpActionResult HandleFMR(FMR fmr)
        {
             using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    UtilityManager.CreateSMTPSendPort(root, fmr);
                    return Ok();
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
            }
        }
    }
}
