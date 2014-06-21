using Biz121.Web.Models;
using Microsoft.BizTalk.ExplorerOM;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Biz121.Web.Utilities;
using System.Web.Http.Description;
using Biz121.Web.ExplorerOM;

namespace Biz121.Web.Api
{
    [RoutePrefix("api/v1")]
    [Route("sp")]
     public class SPController : ApiController
    {
        // GET api/receivelocation

         [ResponseType(typeof(IEnumerable<BizSP>))]
        public IHttpActionResult Get()
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    return Ok(SPManager.GetSendPorts(root));

                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
            }
        }

        [Route("sp/{spName}")]
        [ResponseType(typeof(BizSP))]
        public IHttpActionResult Get(string spName)
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    BizSP port = SPManager.GetSendPort(root, spName);
                    if (port == null)
                        return NotFound();
                    else
                        return Ok(port);
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
            }

        }

        
        public IHttpActionResult Post([FromBody]BizSP port)
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    SPManager.CreateSendPort(root, port);
                    return Ok();
                }
                catch (Exception e)
                {
                    root.DiscardChanges();
                    return BadRequest(e.ToString());
                }
            }

        }

     
       
        // PUT api/receivelocation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/receivelocation/5
        [Route("sp/{spName}")]
        public IHttpActionResult Delete(string spName)
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    SPManager.DeleteSendPort(root, spName);
                    return Ok();
                }
                catch (Exception e)//If it fails, roll-back everything we have done so far
                {
                    root.DiscardChanges();
                    return BadRequest(e.ToString());
                }
            }

        }
    }
}
