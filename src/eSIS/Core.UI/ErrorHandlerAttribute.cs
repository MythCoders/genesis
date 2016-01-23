using System.Web.Mvc;
using eSIS.Core.API;

namespace eSIS.Core.UI
{
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var error = new ApiError();
            //TODO: serialize the error into json data and return to client
            base.OnException(filterContext);
        }
    }
}
