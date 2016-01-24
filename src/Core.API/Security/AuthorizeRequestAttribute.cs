using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.Security.Principal;

namespace eSIS.Core.API.Security
{
    //public class AuthorizeRequestAttribute : AuthorizeAttribute
    //{
    //    protected override bool IsAuthorized(HttpActionContext actionContext)
    //    {
    //        return base.IsAuthorized(actionContext);
    //    }
    //}

    public interface ISecurityService
    {
        bool SetPrincipal(string username, string authorizationToken);
    }

    public class SecuritySecurity : ISecurityService
    {
        //public virtual ISession Sessiong
        public bool SetPrincipal(string username, string authorizationToken)
        {
            throw new NotImplementedException();
        }
    }
}
