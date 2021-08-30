using CollectionApp.WEB.Utils;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace CollectionApp.WEB.Helpers
{
    public static class ItemHelper
    {
        private static void CreateInput(
            StringWriter writer,
            ExtraField extraField)
        {
            var container = new TagBuilder("div");
            container.AddCssClass(extraField.ContainerCssClass);
            if (extraField.ElementType == "hidden")
            {
                container.AddCssClass("d-none");
            }
            var label = new TagBuilder("label");
            label.InnerHtml.Append(extraField.Label);
            label.AddCssClass(extraField.LabelCssClass);
            var input = new TagBuilder(extraField.Element);
            if (extraField.ElementType != "checkbox")
            {
                input.MergeAttribute("required", string.Empty);
            }
            input.MergeAttribute("name", extraField.Name);
            input.AddCssClass(extraField.InputCssClass);
            input.MergeAttribute("type", extraField.ElementType);
            if (extraField.Element == "textarea")
            {
                input.InnerHtml.Append(extraField.Value);
            }
            else if (extraField.ElementType == "checkbox")
            {
                input.MergeAttribute("value", "true");
                if (extraField.Value.Length > 0)
                {
                    input.MergeAttribute(extraField.Value, "");
                }
            }
            else
            {
                input.MergeAttribute("value", extraField.Value);
            }
            if (extraField.ElementType == "number")
            {
                input.MergeAttribute("max", int.MaxValue.ToString());
                input.MergeAttribute("min", int.MinValue.ToString());
                container.InnerHtml.AppendHtml(label);
                container.InnerHtml.AppendHtml(input);
                var validFeedback = new TagBuilder("div");
                validFeedback.AddCssClass("invalid-feedback");
                validFeedback.InnerHtml
                    .Append($"Range: {int.MinValue} ... {int.MaxValue}");
                container.InnerHtml.AppendHtml(validFeedback);
            } else
            {
                container.InnerHtml.AppendHtml(label);
                container.InnerHtml.AppendHtml(input);
            }
            container.WriteTo(writer, HtmlEncoder.Default);
        }

        public static HtmlString AddExtraFields(
            this IHtmlHelper html,
            ItemViewModel model)
        {
            var writer = new StringWriter();
            foreach (var extraField in FieldTypeUtil.GetFields(model))
            {
                CreateInput(writer, extraField);
            }
            return new HtmlString(writer.ToString());
        }
    }
}
