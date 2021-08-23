using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace CollectionApp.WEB.Helpers
{
    public static class ItemHelper
    {
        private static ExtraField CheckOrder(int index, Collection collection)
        {
            var extraField = new ExtraField();
            if (index == 0)
            {
                extraField.Label = collection.FirstFieldName;
                extraField.Name = "First";
            }
            else if (index == 1)
            {
                extraField.Label = collection.SecondFieldName;
                extraField.Name = "Second";
            }
            else if (index == 2)
            {
                extraField.Label = collection.ThirdFieldName;
                extraField.Name = "Third";
            }
            return extraField;
        }

        private static void CreateInput(
            StringWriter writer,
            string element,
            ExtraField extraField,
            string type = "text",
            string containerCssClass = "mb-3",
            string inputCssClass = "form-control",
            string labelCssClass = "form-label")
        {
            var container = new TagBuilder("div");
            container.AddCssClass(containerCssClass);
            var label = new TagBuilder("label");
            label.InnerHtml.Append(extraField.Label);
            label.AddCssClass(labelCssClass);
            var input = new TagBuilder(element);
            input.MergeAttribute("required", string.Empty);
            input.MergeAttribute("name", extraField.Name);
            input.AddCssClass(inputCssClass);
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", extraField.Value);
            container.InnerHtml.AppendHtml(label);
            container.InnerHtml.AppendHtml(input);
            container.WriteTo(writer, HtmlEncoder.Default);
        }

        public static HtmlString AddExtraFields(
            this IHtmlHelper html,
            ItemViewModel model)
        {
            var writer = new System.IO.StringWriter();
            FieldType[] fieldTypes = new FieldType[]
            {
                model.Collection.FirstFieldType,
                model.Collection.SecondFieldType,
                model.Collection.ThirdFieldType
            };
            for (var i = 0; i < fieldTypes.Length; i++)
            {
                var extraField = CheckOrder(i, model.Collection);
                switch (fieldTypes[i])
                {
                    case FieldType.String:
                        extraField.Name += "String";
                        CreateInput(writer, "input", extraField);
                        break;
                    case FieldType.Integer:
                        extraField.Name += "Integer";
                        CreateInput(writer, "input", extraField, "number");
                        break;
                    case FieldType.Markdown:
                        extraField.Name += "Markdown";
                        CreateInput(writer, "textarea", extraField);
                        break;
                    case FieldType.Boolean:
                        extraField.Name += "Boolean";
                        CreateInput(writer, "input", extraField, "checkbox", "form-check",
                            "form-check-input", "form-check-label");
                        break;
                    case FieldType.Date:
                        extraField.Name += "Date";
                        CreateInput(writer, "input", extraField, "date");
                        break;
                }
            }
            CreateInput(writer, "input",
                new ExtraField() 
                { 
                    Label = "",
                    Name = "CollectionId",
                    Value = model.Collection.Id.ToString()
                }, "hidden");
            return new HtmlString(writer.ToString());
        }
    }
}
