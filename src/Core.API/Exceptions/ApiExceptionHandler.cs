using System.IO;
using System.Net;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace eSIS.Core.API.Exceptions
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ApiErrorExcpetion)
            {
                HandleApiException(context);
            }
            else if (context.Exception is RequiredHeaderException)
            {
                HandleRequiredHeaderException(context);
            }
            else if (context.Exception is IOException)
            {
                HandleIoException(context);
            }
            else
            {
                HandleGenericError(context);
            }

            base.Handle(context);
        }

        private static void HandleApiException(ExceptionHandlerContext context)
        {
            var exception = (ApiErrorExcpetion) context.Exception;
            var statusCode = HttpStatusCode.ServiceUnavailable;

            if (exception.OverridenHttpStatusCode.HasValue)
            {
                statusCode = exception.OverridenHttpStatusCode.Value;
            }

            Log(LogLevel.Fatal, context);
            context.Result = new ApiErrorResult(exception, statusCode);
        }

        private static void HandleRequiredHeaderException(ExceptionHandlerContext context)
        {
            var returnValueException = new ApiErrorExcpetion(GenericApiErrorMessage(), context.Exception.Message);
            Log(LogLevel.Info, context);
            context.Result = new ApiErrorResult(returnValueException, HttpStatusCode.NotAcceptable);
        }

        private static void HandleIoException(ExceptionHandlerContext context)
        {
            var returnValueException = new ApiErrorExcpetion(GenericApiErrorMessage(), context.Exception.Message);
            Log(LogLevel.Fatal, context);
            context.Result = new ApiErrorResult(returnValueException, HttpStatusCode.ServiceUnavailable);
        }

        private static void HandleGenericError(ExceptionHandlerContext context)
        {
            var returnValueException = new ApiErrorExcpetion(GenericApiErrorMessage(), context.Exception.Message);
            Log(LogLevel.Error, context);
            context.Result = new ApiErrorResult(returnValueException, HttpStatusCode.InternalServerError);
        }

        private static string GenericApiErrorMessage()
        {
            return ConfigurationHelper.InstanceIsProduction
                                    ? Constants.ApiGenericErrorMessage
                                    : $"{Constants.ApiGenericErrorMessage} Please see System Logs for more details.";
        }

        private static void Log(LogLevel logLevel, ExceptionHandlerContext context)
        {
            var logger = LogManager.GetLogger(context.Exception.Source);
            logger.Log(logLevel, context.Exception);
        }
    }
}
