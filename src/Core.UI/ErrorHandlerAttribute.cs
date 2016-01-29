using System.Web.Mvc;

namespace eSIS.Core.UI
{
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //TODO: serialize the error into json data and return to client
            base.OnException(filterContext);
        }
    }
}
