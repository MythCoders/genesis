using System;
using System.Web.Mvc;

namespace eSIS.Core.UI.Attributes
{
    public enum StatusMessageEnum
    {
        Created = 200,
        Updated = 201,
        Deleted = 202,
        FeedbackSubmitted = 203,
        FileUploaded = 205,
        Unauthorized = 401,
        Error = 500
    }

    public class StatusMessageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var status = filterContext.HttpContext.Request.QueryString["Status"];
            if (!string.IsNullOrWhiteSpace(status))
            {
                StatusMessageEnum statusEnum;
                if (Enum.TryParse(status, out statusEnum))
                {
                    switch (statusEnum)
                    {
                        case StatusMessageEnum.Created:
                            filterContext.Controller.ViewBag.Success = "Record was created successfully.";
                            break;
                        case StatusMessageEnum.Updated:
                            filterContext.Controller.ViewBag.Success = "Record was saved successfully.";
                            break;
                        case StatusMessageEnum.Deleted:
                            filterContext.Controller.ViewBag.Success = "Record was deleted successfully.";
                            break;
                        case StatusMessageEnum.FeedbackSubmitted:
                            filterContext.Controller.ViewBag.Success = "Your feedback was submitted successfully. If we have any questions, we will contact you within 24-48 hours.";
                            break;
                        case StatusMessageEnum.Unauthorized:
                            filterContext.Controller.ViewBag.Error = "You are not authorized to perform this action.";
                            break;
                        case StatusMessageEnum.Error:
                            filterContext.Controller.ViewBag.Error = "An unexpected error has occurred.";
                            break;
                        case StatusMessageEnum.FileUploaded:
                            filterContext.Controller.ViewBag.Success = "File was uploaded successfully.";
                            break;
                    }

                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}