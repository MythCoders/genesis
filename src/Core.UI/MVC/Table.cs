using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MC.eSIS.Core.UI.MVC
{
    public static partial class TableExtensions
    {
        public static MvcHtmlString Table<T, TW>(this HtmlHelper<T> helper, IEnumerable<TW> collection, TableTypeEnum type, string name)
        {
            return Table(helper, collection, type, null, null, name);
        }

        public static MvcHtmlString Table<T, TW>(this HtmlHelper<T> helper, IEnumerable<TW> collection, TableTypeEnum type, Func<TW, object> propertySelector, string name)
        {
            return Table(helper, collection, type, propertySelector, null, name);
        }

        public static MvcHtmlString Table<T, TW>(this HtmlHelper<T> helper, IEnumerable<TW> collection, TableTypeEnum type, Func<TW, object> propertySelector, object attributes, string name)
        {
            return Table(helper, collection, type, propertySelector, new RouteValueDictionary(attributes), name);
        }

        public static MvcHtmlString Table<T, TW>(this HtmlHelper<T> helper, IEnumerable<TW> collection, TableTypeEnum type, Func<TW, object> propertySelector, IDictionary<string, object> attributes, string name)
        {
            var definition = new TableDefinition<TW>();
            definition.Name = name;
            definition.Type = type;
            definition.Attributes = attributes;
            definition.Data = collection.ToList();
            definition.PropertySelector = propertySelector;

            var panel = TableExtensions.CreatePanel(name);
            var panelFooter = new TagBuilder("div");
            panelFooter.MergeAttribute("class", "panel-footer");

            if (definition.RecordsToDisplay().Any())
            {
                var table = BuildTableBase(helper, definition);
                panel.InnerHtml += table;
            }

            panelFooter.InnerHtml += RecordCounts(definition.RecordsToDisplay().Count);
            panel.InnerHtml += panelFooter;

            return new MvcHtmlString(panel.ToString());
        }

        private static MvcHtmlString RecordCounts(int records)
        {
            var p = new TagBuilder("p");
            p.MergeAttribute("class", "text-info text-right");
            p.InnerHtml = records == 0 ? "No results found." : records == 1 ? "1 result found." : string.Format("{0} results found.", records);
            return new MvcHtmlString(p.ToString());
        }
    }
}
