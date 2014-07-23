using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.WebPages;


namespace Biz121.Web
{
   public class AuthorizationConfig
    {
        ///Initializes the roles.
        public static void Initialize()
        {
            try
            {

                if ( !Roles.RoleExists( "Administrator" ) )
                {
                    Roles.CreateRole( "Administrator" );
                    Roles.AddUserToRole("HQDWESB02\\svc_biztalk", "Administrator");
                    //Roles.RemoveUserFromRole("HQDWESB02\\svc_biztalk", "Administrator");
                }

                if (!Roles.IsUserInRole("Administrator"))
                {
                    Roles.AddUserToRole(HttpContext.Current.User.Identity.Name, "Administrator");
                }
              

            }
            catch ( Exception e )
            {
                throw e;
            }
        }
    }
}