using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eSIS.Core.UI.Classes;

namespace eSIS.Core.UI.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class BreadcrumbAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private string _area;
        private string _controller;
        private string _action;
        private readonly bool _removeParameter;

        public BreadcrumbAttribute(string name)
        {
            _name = name;
        }

        public BreadcrumbAttribute(string name, string action) : this(name, action, false)
        {
            _name = name;
            _action = action;
        }

        public BreadcrumbAttribute(string name, string action, bool removeParameter)
        {
            _name = name;
            _action = action;
            _removeParameter = removeParameter;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _area = filterContext.RequestContext.RouteData.DataTokens["area"].ToString();
            _controller = filterContext.RequestContext.RouteData.GetRequiredString("controller");
            var dict = filterContext.RequestContext.RouteData.Values;

            //Allow for action override
            if (string.IsNullOrWhiteSpace(_action))
            {
                _action = filterContext.RequestContext.RouteData.GetRequiredString("action");
            }

            if (filterContext.Controller.ViewBag.Breadcrumbs == null)
            {
                filterContext.Controller.ViewBag.Breadcrumbs = new List<Breadcrumb>();
            }

            ((List<Breadcrumb>) filterContext.Controller.ViewBag.Breadcrumbs).Add(_removeParameter
                ? new Breadcrumb {Name = _name, Area = _area, Controller = _controller, Action = _action}
                : new Breadcrumb
                {
                    Name = _name,
                    Area = _area,
                    Controller = _controller,
                    Action = _action,
                    Routes = dict
                });

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }
    }
}