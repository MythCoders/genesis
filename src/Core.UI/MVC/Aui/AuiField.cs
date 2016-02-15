using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eSIS.Core.UI.MVC.Aui
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
