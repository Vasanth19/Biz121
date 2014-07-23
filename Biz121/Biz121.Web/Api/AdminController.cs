using System.Web.Security;
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
using System.Web.Http.Description;

namespace Biz121.Web.Api
{
    [RoutePrefix("api/v1")]
    [Route("admin")]
    public class AdminController : ApiController
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"> Format "HQDWESB02\\svc_biztalk"</param>
        /// <param name="role"> Administrator</param>
        /// <returns></returns>
       [Route("admin/adduser/{role}")]
       [HttpPost]
        public IHttpActionResult AddUser([FromBody]string user, [FromUri]string role)
        {
                try
                {
                    if (!Roles.RoleExists(role))
                    {
                        Roles.CreateRole(role);
                        Roles.AddUserToRole(user, role);
                        //Roles.RemoveUserFromRole("HQDWESB02\\svc_biztalk", "Administrator");
                    }
                    else
                    {
                        Roles.AddUserToRole(user, role);
                    }
                    return Ok();
                }
                catch (Exception e)//If it fails, roll-back all changes.
                {
                    return BadRequest(e.ToString());
                }
           
        }

       [Route("admin/removeuser")]
       [HttpPost]
       public IHttpActionResult RemoveUserFromRole(string user, string role)
       {
           try
           {
               if (Roles.RoleExists(role))
               {
                   Roles.RemoveUserFromRole(user, role);
               }
               else
               {
                   return NotFound();
               }
               return Ok();
           }
           catch (Exception e)//If it fails, roll-back all changes.
           {
               return BadRequest(e.ToString());
           }

       }

   }
}
