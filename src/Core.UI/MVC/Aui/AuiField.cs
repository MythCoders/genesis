using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MC.eSIS.Core.UI.MVC.Aui
{
    public static partial class AuiHelpers
    {
        public static MvcHtmlString EditorTextBoxFor<TModel, TField>(this HtmlHelper<TModel> html, Expression<Func<TModel, TField>> expression)
        {
            var fieldGroup = new TagBuilder("div");
            fieldGroup.AddCssClass("field-group");

            return new MvcHtmlString(fieldGroup.ToString());
        }
    }
}
