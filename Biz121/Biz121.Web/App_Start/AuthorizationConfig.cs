using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Biz121.Web
{
   public class AuthorizationConfig
    {
        ///Initializes the roles.
        public static void Initialize()
        {
            try
            {
                Roles.AddUserToRole("SIVA\\Vasanth", "Administrator");
                if ( !Roles.RoleExists( "Administrator" ) )
                {
                    Roles.CreateRole( "Administrator" );
                    Roles.AddUserToRole("HQDWESB02\\svc_biztalk", "Administrator");
                    //Roles.RemoveUserFromRole("HQDWESB02\\svc_biztalk", "Administrator");
                }
            }
            catch ( Exception e )
            {
                throw e;
            }
        }
    }
}