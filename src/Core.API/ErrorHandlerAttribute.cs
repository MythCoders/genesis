using System.Web.Mvc;

namespace eSIS.Core.API
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
