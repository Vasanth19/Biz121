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
    [Route("testrp")]
     public class TestRPController : ApiController
    {
        // GET api/receivelocation

        [ResponseType(typeof(IEnumerable<BizRP>))]
        public IHttpActionResult Get()
        {

            List<BizRP> listPorts = new List<BizRP>();
            List<BizRL> listlocations;
            for (int j = 0; j < 3; j++)
            {
                listlocations = new List<BizRL>();
            int i = 0;
            while (i < 2)
            {
                listlocations.Add(
                new BizRL()
                {
                    Address = @"C:\File\File.txt" + i,
                    Name = "Receive Location " + i,
                    PipelineName = "PassThru Pipeline" + i,
                    TransportType = "File",
                    TransportTypeData = "<Group><FileNet>5</FileNet></Group>"
                }
                );
                i++;
            }
            BizRP port = new BizRP()
                {
                    ApplicationName = "Application 1",
                    Description = "Test Adapter" + j,
                    Name = "Receive Port " + j,
                    PrimaryRL = "Receive Location " + j,
                    RLCount = 2,
                    RLs = listlocations

                };
                listPorts.Add(port);

            }
            return Ok(listPorts);
             
            
        }

        [Route("testrp/{rpName}")]
        [ResponseType(typeof(BizRP))]
        public IHttpActionResult Get(string rpName)
        {
            List<BizRL> listlocations = new List<BizRL>(); int i = 0;
            while (i < 2)
            {
                listlocations.Add(
                new BizRL()
                {
                    Address = @"C:\File\File.txt" + i,
                    Name = "Receive Location " + i,
                    PipelineName = "PassThru Pipeline" + i,
                    TransportType = "File",
                    TransportTypeData = "<Group><FileNet>5</FileNet></Group>"
                }
                );
                i++;
            }
            BizRP port = new BizRP()
            {
                ApplicationName = "Application 1",
                Description = "Test Adapter" ,
                Name = "Receive Port" ,
                PrimaryRL = "Receive Location " ,
                RLCount = 2,
                RLs = listlocations

            };

            return Ok(port);

        }

        
        public IHttpActionResult Post([FromBody]BizRP port)
        {
            
         
            try
            {
      
                return Ok();
            }
            catch (Exception e)
            {
           
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
          return Ok();
        }
    }
}
