using System.Web.Mvc;
using NLog;

namespace MC.eSIS.Core.UI
{
    public class UiHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var logger = LogManager.GetLogger(filterContext.Exception.Source);
            logger.Error(filterContext.Exception);

            base.OnException(filterContext);
        }
    }
}
