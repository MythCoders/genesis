using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using eSIS.Core.API.Exceptions;

namespace eSIS.Core.API
{
    public class ErrorHandlerAttribute : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is RequiredHeaderException)
            {
                var errorMessage = context.Exception as RequiredHeaderException;
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.NotAcceptable, new ApiError(errorMessage.Message)));
            }
            else if (context.Exception is ValidationException)
            {
                var errorMessage = context.Exception as ValidationException;
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage.PropertyErrors));
            }
            //else if (context.Exception is FaultException<SessionFault>)
            //{
            //    var errorMessage = context.Exception as FaultException<SessionFault>;
            //    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.Unauthorized, new ApiError(errorMessage.Detail.SessionFaultType.ToString())));
            //}
            //else if (context.Exception is FaultException<PermissionFault>)
            //{
            //    var errorMessage = context.Exception as FaultException<PermissionFault>;
            //    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.Unauthorized, new ApiError(errorMessage.Detail.Message)));
            //}
            //else if (context.Exception is FaultException<WebsiteDisabledFault>)
            //{
            //    var errorMessage = context.Exception as FaultException<WebsiteDisabledFault>;
            //    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, new ApiError(errorMessage.Detail.Message)));
            //}
            else
            {
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ApiError("An unexpected error has occurred and been logged.  If this problem persists, please contact your system administrator.")));
            }
        }
    }
}
