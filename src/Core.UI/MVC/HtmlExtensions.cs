using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace eSIS.Core.UI.MVC
{
    public static class Helpers
    {
        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="roles">The Enum Values of the roles for which to check membership</param>
        /// <returns>true if the current principal is a member of the specified role; otherwise, false.</returns>
        public static bool IsInRole(this IPrincipal principal, params Enum[] roles)
        {
            return roles.Any(p => principal.IsInRole(p.ToString()));
        }

        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string specificStringToReplace = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model);

            model = model.Replace(specificStringToReplace ?? "\r\n", "<br />\r\n");

            if (string.IsNullOrWhiteSpace(model))
            {
                return MvcHtmlString.Empty;
            }

            return MvcHtmlString.Create(model);
        }

        public static MvcHtmlString DisabledForField<TModel, TField>(this HtmlHelper<TModel> html, Expression<Func<TModel, TField>> expression, int labelSpan, int fieldSpan)
        {
            if (labelSpan + fieldSpan > 12)
            {
                throw new Exception("Rows can not span more than 12 columns");
            }

            var formGroup = new TagBuilder("div");
            formGroup.MergeAttribute("class", "form-group");

            formGroup.InnerHtml += CreateFormGroupContents(html, expression, labelSpan, fieldSpan, false, true, "");

            return new MvcHtmlString(formGroup.ToString());
        }

        public static MvcHtmlString EditorForField<TModel, TField>(this HtmlHelper<TModel> html, Expression<Func<TModel, TField>> expression, int labelSpan, int fieldSpan, string templateName = "")
        {
            if (labelSpan + fieldSpan > 12)
            {
                throw new Exception("Rows can not span more than 12 columns");
            }

            var formGroup = new TagBuilder("div");
            formGroup.MergeAttribute("class", "form-group");

            formGroup.InnerHtml += CreateFormGroupContents(html, expression, labelSpan, fieldSpan, false, false, templateName);

            return new MvcHtmlString(formGroup.ToString());
        }

        public static MvcHtmlString EditorForFields<TModel, TField, TSecondField>(this HtmlHelper<TModel> html, Expression<Func<TModel, TField>> expression, Expression<Func<TModel, TSecondField>> secopndExpression, int labelSpan, int fieldSpan)
        {
            if ((labelSpan + fieldSpan) * 2 > 12)
            {
                throw new Exception("Rows can not span more than 12 columns");
            }

            var formGroup = new TagBuilder("div");
            formGroup.MergeAttribute("class", "form-group");

            formGroup.InnerHtml += CreateFormGroupContents(html, expression, labelSpan, fieldSpan, false, false, "");
            formGroup.InnerHtml += CreateFormGroupContents(html, secopndExpression, labelSpan, fieldSpan, false, false, "");

            return new MvcHtmlString(formGroup.ToString());
        }

        public static MvcHtmlString DisplayForFields<TModel, TField, TSecondField>(this HtmlHelper<TModel> html, Expression<Func<TModel, TField>> expression, Expression<Func<TModel, TSecondField>> secopndExpression, int labelSpan, int fieldSpan)
        {
            if ((labelSpan + fieldSpan) * 2 > 12)
            {
                throw new Exception("Rows can not span more than 12 columns");
            }

            var formGroup = new TagBuilder("div");
            formGroup.MergeAttribute("class", "form-group");

            formGroup.InnerHtml += CreateFormGroupContents(html, expression, labelSpan, fieldSpan, true, false, "");
            formGroup.InnerHtml += CreateFormGroupContents(html, secopndExpression, labelSpan, fieldSpan, true, false, "");

            return new MvcHtmlString(formGroup.ToString());
        }

        public static MvcHtmlString DisplayForField<T, TModel>(this HtmlHelper<T> helper, Expression<Func<T, TModel>> expression, int labelSpan, int fieldSpan, string templateName = "")
        {
            if (labelSpan + fieldSpan > 12)
            {
                throw new Exception("Row length can not be greater than 12");
            }

            var group = new TagBuilder("div");
            group.MergeAttribute("class", "form-group");

            group.InnerHtml += CreateFormGroupContents(helper, expression, labelSpan, fieldSpan, true, false, templateName);

            return new MvcHtmlString(group.ToString());
        }

        private static MvcHtmlString CreateFormGroupContents<TModel, TField>(HtmlHelper<TModel> helper, Expression<Func<TModel, TField>> expression, int labelSpan, int fieldSpan, bool useDisplay, bool disabledField, string templateName)
        {
            var label = helper.LabelFor(expression, new { @class = string.Format("col-md-{0}", labelSpan), style = "font-weight: bold;" });

            var field = new TagBuilder("div");
            field.MergeAttribute("class", string.Format("col-md-{0}", fieldSpan));

            object additionalViewData;

            if (disabledField)
            {
                additionalViewData = new { htmlAttributes = new { @class = "form-control", @disabled = "disabled" } };
            }
            else
            {
                additionalViewData = new { htmlAttributes = new { @class = "form-control" } };
            }

            if (!useDisplay)
            {
                if (string.IsNullOrWhiteSpace(templateName))
                {
                    field.InnerHtml += helper.EditorFor(expression, additionalViewData);
                }
                else
                {
                    field.InnerHtml += helper.EditorFor(expression, templateName, additionalViewData);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(templateName))
                {
                    field.InnerHtml += helper.DisplayFor(expression, additionalViewData);
                }
                else
                {
                    field.InnerHtml += helper.DisplayFor(expression, templateName, additionalViewData);
                }

                field.InnerHtml += helper.HiddenFor(expression);
            }

            field.InnerHtml += helper.ValidationMessageFor(expression);

            return new MvcHtmlString(label + field.ToString());
        }

        public static MvcHtmlString EditorForZipCode<T, TFiveDigit, TFourDigit>(this HtmlHelper<T> helper, Expression<Func<T, TFiveDigit>> fiveDigit, Expression<Func<T, TFourDigit>> fourDigit, int displaySpan, int fieldSpan)
        {
            var group = new TagBuilder("div");
            group.MergeAttribute("class", "form-group");

            var label = new TagBuilder("div");
            label.MergeAttribute("class", string.Format("col-md-{0}", displaySpan));
            label.InnerHtml += helper.LabelFor(fiveDigit);

            var field = new TagBuilder("div");
            field.MergeAttribute("class", string.Format("col-md-{0}", fieldSpan));

            var inputGroup = new TagBuilder("div");
            inputGroup.MergeAttribute("class", "input-group");
            inputGroup.InnerHtml += helper.EditorFor(fiveDigit);
            inputGroup.InnerHtml += "<span class=\"input-group-addon\">-</span>";
            inputGroup.InnerHtml += helper.EditorFor(fourDigit);

            field.InnerHtml += inputGroup.ToString();
            field.InnerHtml += helper.ValidationMessageFor(fiveDigit);
            field.InnerHtml += helper.ValidationMessageFor(fourDigit);

            group.InnerHtml += label.ToString();
            group.InnerHtml += field.ToString();

            return new MvcHtmlString(group.ToString());
        }
    }
}
