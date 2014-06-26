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
    [Route("rp")]
    [Authorize(Roles = "Administrator")]
     public class RPController : ApiController
    {
        // GET api/receivelocation

        [ResponseType(typeof(IEnumerable<BizRP>))]
        public IHttpActionResult Get()
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    return Ok(RPManager.GetReceivePorts(root));
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
            }
        }

        [Route("rp/{rpName}")]
        [ResponseType(typeof(BizRP))]
        public IHttpActionResult Get(string rpName)
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {
                try
                {
                    BizRP port = RPManager.GetReceivePort(root,rpName);
                    if(port == null)
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

        
        public IHttpActionResult Post([FromBody]BizRP port)
        {
            
            BtsCatalogExplorer root = new BtsCatalogExplorer();
            try
            {
                RPManager.CreateReceivePort(root, port);
                return Ok();
            }
            catch (Exception e)
            {
                root.DiscardChanges();
                return BadRequest(e.ToString());
            }

        }


        // PUT api/receivelocation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/receivelocation/5
        [Route("rp/{rpName}")]
        public IHttpActionResult Delete(string rpName)
        {
            using (BtsCatalogExplorer root = new BtsCatalogExplorer())
            {

                try
                {
                    RPManager.DeleteReceivePort(root,rpName);
                }
                catch (Exception e)//If it fails, roll-back everything we have done so far
                {
                    root.DiscardChanges();
                    return BadRequest(e.ToString());
                }
            }
            return Ok();
        }
    }
}
