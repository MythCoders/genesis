using System;
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
            //TODO: log the exception!

            ApiError apiError;
            var isProduction = string.Compare(ConfigurationHelper.GetByKey("IsProduction"), "false", StringComparison.CurrentCultureIgnoreCase) == 0;

            if (context.Exception is RequiredHeaderException)
            {
                var errorMessage = context.Exception as RequiredHeaderException;
                apiError = new ApiError(errorMessage.Message);
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.NotAcceptable, apiError));
            }
            else if (context.Exception is ValidationException)
            {
                var errorMessage = context.Exception as ValidationException;
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage.PropertyErrors));
            }
            else
            {
                var genericMessage = "An unexpected error has occurred and been logged. If this problem persists, please contact your system administrator.";
                apiError = isProduction ? new ApiError(genericMessage, context.Exception.Message) : new ApiError(genericMessage);
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, apiError));
            }
        }
    }
}
