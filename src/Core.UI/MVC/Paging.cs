using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;

namespace eSIS.Core.UI.MVC
{
    public static class Paging
    {
        /// <summary>
        /// Applies standard grid properties such as: Data Source configuration, page size and refresh controls.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gridBuilder">Do you even see this?</param>
        /// <param name="controllerName">Controller name for the method this grid will use to send Ajax requests to receive data</param>
        /// <param name="areaName">Area name for the method this grid will use to send Ajax requests to receive data</param>
        /// <param name="actionName"> Action name for the method this grid will use to send Ajax requests to receive data. </param>
        /// <returns></returns>
        public static GridBuilder<T> StandardPagingGrid<T>(this GridBuilder<T> gridBuilder,
            [AspMvcController] string controllerName,
            [AspMvcArea] string areaName,
            [AspMvcAction] string actionName = "Index_Query")
            where T : class
        {
            return gridBuilder
                .NoRecords("No records found.")
                .Pageable(p => p.Refresh(true).PageSizes(true))
                .Sortable()
                .Scrollable(s => s.Virtual(true))
                .Filterable(ftb => ftb.Mode(GridFilterMode.Row))
                .Reorderable(r => r.Columns(true))
                .DataSource(ds =>
                {
                    ds.Ajax().Read(read => read.Action(actionName, controllerName, new { area = areaName }));
                })
                .Deferred();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="factory"></param>
        /// <param name="expression"></param>
        /// <param name="linkText"></param>
        /// <param name="url"></param>
        /// <param name="cssClass"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static GridBoundColumnBuilder<TModel> NavigationButton<TModel, TValue>(this GridColumnFactory<TModel> factory,
            Expression<Func<TModel, TValue>> expression,
            string linkText,
            string url,
            string cssClass = "",
            int width = 80)
            where TModel : class
        {
            return factory.Bound(expression)
                .ClientTemplate($"<a class='{cssClass}' href='{url}{HtmlTemplateProperty("Id")}'>{linkText}</a>")
                .Filterable(false)
                .Width(width);
        }

        public static GridBoundColumnBuilder<TModel> Raw<TModel, TValue>(this GridColumnFactory<TModel> factory,
            Expression<Func<TModel, TValue>> expression,
            string html,
            int width = 80)
            where TModel : class
        {
            return factory.Bound(expression)
                .ClientTemplate(html)
                .Filterable(false)
                .Width(width);
        }

        internal static string HtmlTemplateProperty(string propertyName)
        {
            return $"/#={propertyName}#";
        }
    }
}
