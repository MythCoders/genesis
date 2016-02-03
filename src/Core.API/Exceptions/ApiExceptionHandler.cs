using System.Data.Entity.Infrastructure;
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
                var exception = (ApiErrorExcpetion)context.Exception;
                HandleError(context, LogLevel.Fatal, exception.OverridenHttpStatusCode ?? HttpStatusCode.ServiceUnavailable);
            }
            else if (context.Exception is RequiredHeaderException)
            {
                HandleError(context, LogLevel.Info, HttpStatusCode.NotAcceptable);
            }
            else if (context.Exception is IOException)
            {
                HandleError(context, LogLevel.Fatal, HttpStatusCode.ServiceUnavailable);
            }
            else if (context.Exception is DbUpdateConcurrencyException)
            {
                HandleError(context, LogLevel.Debug, HttpStatusCode.Conflict);
            }
            else
            {
                HandleError(context, LogLevel.Error, HttpStatusCode.InternalServerError);
            }

            base.Handle(context);
        }

        private static void HandleError(ExceptionHandlerContext context, LogLevel logLevel, HttpStatusCode httpStatusCode)
        {
            var returnValueException = new ApiErrorExcpetion(GenericApiErrorMessage(), context.Exception.Message);
            Log(logLevel, context);
            context.Result = new ApiErrorResult(returnValueException, httpStatusCode);
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
