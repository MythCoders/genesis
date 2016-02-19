using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MC.eSIS.Core.Helpers;

// ReSharper disable Mvc.ActionNotResolved

namespace MC.eSIS.Core.UI.MVC
{
    public static partial class TableExtensions
    {
        /// <summary>
        /// Creates the panel for which all Table and TableWithPaging content will be contained inside
        /// </summary>
        /// <param name="tableName">Name of Panel. Text will be placed inside the Panel Heading</param>
        /// <returns></returns>
        private static TagBuilder CreatePanel(string tableName)
        {
            var returnValue = new TagBuilder("div");
            returnValue.MergeAttribute("class", "panel panel-primary");

            if (!string.IsNullOrWhiteSpace(tableName))
            {
                var panelHeader = new TagBuilder("div");
                panelHeader.MergeAttribute("class", "panel-heading");
                panelHeader.InnerHtml = tableName;
                returnValue.InnerHtml += panelHeader.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// Builds the table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="helper"></param>
        /// <param name="tableDefinition"></param>
        /// <returns></returns>
        private static TagBuilder BuildTableBase<T, TData>(HtmlHelper<T> helper, TableDefinition<TData> tableDefinition)
        {
            var returnValue = new TagBuilder("table");

            if (tableDefinition.Attributes == null || tableDefinition.Attributes.Count == 0)
            {
                returnValue.MergeAttribute("class", "table table-striped table-bordered");
            }
            else
            {
                returnValue.MergeAttributes(tableDefinition.Attributes);
            }

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var header = new TagBuilder("thead");
            var headerRow = new TagBuilder("tr");

            foreach (var property in tableDefinition.RecordsToDisplay().First().GetType().GetProperties().Where(property => !IsIgnored(property)))
            {
                var realProperty = tableDefinition.Data.First().GetType().GetProperties().ToList().FirstOrDefault(m => m.Name == property.Name) ?? property;
                tableDefinition.Properties.Add(realProperty); //to minimize the number of times we must use reflection, we 'cache' the PropertyInfo in the TableDefinition
                headerRow.InnerHtml += CreateColumnHeaders(tableDefinition, realProperty);
            }

            header.InnerHtml += string.Format("{0}\t\t{1}", Environment.NewLine, headerRow);
            returnValue.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, header);

            var body = new TagBuilder("tbody");

            foreach (var item in tableDefinition.RecordsToDisplay())
            {
                var bodyRow = new TagBuilder("tr");

                foreach (var property in tableDefinition.Properties)
                {
                    MvcHtmlString cell;

                    if (IsKey(property) && tableDefinition.Type != TableTypeEnum.Readonly)
                    {
                        cell = CreateRowLink(tableDefinition.Type, urlHelper, property, item);
                    }
                    else
                    {
                        cell = GetCellValue(helper, property, item);
                    }

                    bodyRow.InnerHtml += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
                }

                body.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, bodyRow);
            }

            returnValue.InnerHtml += string.Format("{0}\t{1}", Environment.NewLine, body);

            return returnValue;
        }

        /// <summary>
        /// Creates the headers for every column. If a specific column is sortable, the links will automatically be created
        /// </summary>
        /// /// <typeparam name="T"></typeparam>
        /// <param name="tableDefinition"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static MvcHtmlString CreateColumnHeaders<T>(TableDefinition<T> tableDefinition, PropertyInfo property)
        {
            var returnValue = new TagBuilder("th");

            if (IsKey(property))
            {
                if (tableDefinition.Type.IsIn(TableTypeEnum.View, TableTypeEnum.ViewEdit, TableTypeEnum.Select, TableTypeEnum.Edit))
                {
                    returnValue.MergeAttribute("class", "Detail");
                }

                if (tableDefinition.Type == TableTypeEnum.ViewEdit)
                {
                    returnValue.MergeAttribute("class", "Edit");
                }
            }
            else
            {
                var displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() as DisplayAttribute;
                var displayName = displayAttribute != null
                    ? displayAttribute.Name
                    : property.Name.Replace("_", " ");

                returnValue.InnerHtml = displayName;

                returnValue.MergeAttribute("class", property.Name);

                if (IsNumeric(property.PropertyType))
                {
                    returnValue.MergeAttributes(GetNumericFormatting());
                }
            }

            return new MvcHtmlString(returnValue.ToString());
        }

        /// <summary>
        /// Creates the click-able links on the right hand side of every row
        /// </summary>
        /// <param name="type"></param>
        /// <param name="urlHelper"></param>
        /// <param name="property"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static MvcHtmlString CreateRowLink(TableTypeEnum type, UrlHelper urlHelper, PropertyInfo property, object item)
        {
            var returnValue = string.Empty;

            if (type == TableTypeEnum.Select)
            {
                var cell = new TagBuilder("td");
                cell.MergeAttribute("class", "Detail");
                var link = new TagBuilder("a");
                link.MergeAttribute("href", urlHelper.Action("Detail", new { id = GetPropertyValue(property, item) }));
                link.MergeAttribute("onClick", "ODPS.Helper.ShowLoader();");
                link.InnerHtml = "Select";
                cell.InnerHtml += link.ToString();
                returnValue += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
            }

            if (type == TableTypeEnum.ViewHistory)
            {
                var cell = new TagBuilder("td");
                cell.MergeAttribute("class", "Detail");
                var link = new TagBuilder("a");
                link.MergeAttribute("href", urlHelper.Action("HistoryDetail", new { id = property.GetValue(item).ToString() }));
                link.MergeAttribute("onClick", "ODPS.Helper.ShowLoader();");
                link.InnerHtml = "Select";
                cell.InnerHtml += link.ToString();
                returnValue += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
            }

            if (type == TableTypeEnum.View || type == TableTypeEnum.ViewEdit)
            {
                var cell = new TagBuilder("td");
                cell.MergeAttribute("class", "Detail");
                var link = new TagBuilder("a");
                link.MergeAttribute("href", urlHelper.Action("Detail", new { id = GetPropertyValue(property, item) }));
                link.MergeAttribute("data-url", urlHelper.Action("Detail", new { id = GetPropertyValue(property, item) }));
                link.MergeAttribute("class", "odpsModal");
                link.InnerHtml = "View";
                cell.InnerHtml += link.ToString();
                returnValue += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
            }

            if (type == TableTypeEnum.Edit || type == TableTypeEnum.ViewEdit)
            {
                var cell = new TagBuilder("td");
                cell.MergeAttribute("class", "Edit");
                var link = new TagBuilder("a");
                link.MergeAttribute("class", "text-warning");
                link.MergeAttribute("onClick", "ODPS.Helper.ShowLoader();");
                link.MergeAttribute("href", urlHelper.Action("Edit", new { id = GetPropertyValue(property, item) }));
                link.InnerHtml = "Edit";
                cell.InnerHtml += link.ToString();
                returnValue += string.Format("{0}\t\t{1}", Environment.NewLine, cell);
            }

            return new MvcHtmlString(returnValue);
        }

        /// <summary>
        /// Given a variety of different factors, gets the value to display for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="property"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static MvcHtmlString GetCellValue<T>(HtmlHelper<T> helper, PropertyInfo property, object item)
        {
            var cell = new TagBuilder("td");
            cell.MergeAttribute("class", property.Name);

            if (IsNumeric(property.PropertyType))
            {
                cell.MergeAttributes(GetNumericFormatting());
            }

            var value = GetPropertyValue(property, item);
            var valueAsEnum = value as Enum;

            var cellValue = string.Empty;
            if (valueAsEnum != null)
            {
                cellValue += valueAsEnum.GetDescription();
            }
            else if (property.PropertyType == typeof(bool))
            {
                var valueAsBool = (bool)value;
                cellValue = valueAsBool
                    ? "Yes"
                    : "No";
            }
            else if (HasUiHintDefined(property) && value != null)
            {
                var attribute = (UIHintAttribute)property.GetCustomAttributes(typeof(UIHintAttribute), true).Single();
                cellValue += helper.DisplayFor(p => value, attribute.UIHint);
            }
            else if (HasDisplayFormatDefined(property))
            {
                var attribute = (DisplayFormatAttribute)property.GetCustomAttributes(typeof(DisplayFormatAttribute), true).Single();
                cellValue = attribute.NullDisplayText;
            }
            else if (value == null)
            {
                cellValue = string.Empty;
            }
            else
            {
                cellValue = value.ToString();
            }

            cell.InnerHtml += cellValue;
            return new MvcHtmlString(cell.ToString());
        }

        #region Property Helpers

        private static object GetPropertyValue(PropertyInfo property, object item)
        {
            var itemType = item.GetType();
            var origionalProp = itemType.GetProperty(property.Name);
            return origionalProp.GetValue(item);
        }

        public static bool IsKey(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() != null;
        }

        public static bool IsIgnored(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ScaffoldColumnAttribute), true).FirstOrDefault() != null;
        }

        public static bool HasDisplayFormatDefined(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(DisplayFormatAttribute), true).FirstOrDefault() != null;
        }

        public static bool HasUiHintDefined(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(UIHintAttribute), true).FirstOrDefault() != null;
        }

        public static bool IsNumeric(Type type)
        {
            return (Nullable.GetUnderlyingType(type) ?? type).IsIn(
                typeof(int),
                typeof(double),
                typeof(decimal),
                typeof(long),
                typeof(short),
                typeof(sbyte),
                typeof(byte),
                typeof(ulong),
                typeof(ushort),
                typeof(uint),
                typeof(float)
                );
        }

        public static Dictionary<string, string> GetNumericFormatting()
        {
            return new Dictionary<string, string> { { "style", "text-align: right" } };
        }

        #endregion
    }
}
