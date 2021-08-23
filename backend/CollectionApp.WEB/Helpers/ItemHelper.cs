using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using CollectionApp.WEB.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace CollectionApp.WEB.Helpers
{
    public static class ItemHelper
    {
        private static ExtraField CheckOrder(int index, Collection collection, string[] values)
        {
            var extraField = new ExtraField();
            if (index == 0)
            {
                extraField.Label = collection.FirstFieldName;
                extraField.Name = "First";
                extraField.Value = values[index];
            }
            else if (index == 1)
            {
                extraField.Label = collection.SecondFieldName;
                extraField.Name = "Second";
                extraField.Value = values[index];
            }
            else if (index == 2)
            {
                extraField.Label = collection.ThirdFieldName;
                extraField.Name = "Third";
                extraField.Value = values[index];
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
            if (type == "hidden")
            {
                container.AddCssClass("d-none");
            }
            var label = new TagBuilder("label");
            label.InnerHtml.Append(extraField.Label);
            label.AddCssClass(labelCssClass);
            var input = new TagBuilder(element);
            if (type != "checkbox")
            {
                input.MergeAttribute("required", string.Empty);
            }
            input.MergeAttribute("name", extraField.Name);
            input.AddCssClass(inputCssClass);
            input.MergeAttribute("type", type);
            if (element == "textarea")
            {
                input.InnerHtml.Append(extraField.Value);
            }
            else if (type == "checkbox")
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
            if (type == "number")
            {
                input.MergeAttribute("max", Int32.MaxValue.ToString());
                input.MergeAttribute("min", Int32.MinValue.ToString());
                container.InnerHtml.AppendHtml(label);
                container.InnerHtml.AppendHtml(input);
                var validFeedback = new TagBuilder("div");
                validFeedback.AddCssClass("invalid-feedback");
                validFeedback.InnerHtml
                    .Append($"Range: {Int32.MinValue.ToString()} ... {Int32.MaxValue.ToString()}");
                container.InnerHtml.AppendHtml(validFeedback);
            } else
            {
                container.InnerHtml.AppendHtml(label);
                container.InnerHtml.AppendHtml(input);
            }
            container.WriteTo(writer, HtmlEncoder.Default);
        }

        private static void AddStringInput(
            int index,
            ItemViewModel model,
            StringWriter writer)
        {
            var stringField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstString,
                model.SecondString,
                model.ThirdString
            });
            stringField.Name += "String";
            CreateInput(writer, "input", stringField);
        }

        private static void AddIntegerInput(
           int index,
           ItemViewModel model,
           StringWriter writer)
        {
            var integerField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstInteger.ToString(),
                model.SecondInteger.ToString(),
                model.ThirdInteger.ToString()
            });
            integerField.Name += "Integer";
            CreateInput(writer, "input", integerField, "number");
        }


        private static void AddMarkdownInput(
           int index,
           ItemViewModel model,
           StringWriter writer)
        {
            var markdownField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstText,
                model.SecondText,
                model.ThirdText
            });
            markdownField.Name += "Text";
            CreateInput(writer, "textarea", markdownField);
        }

        private static void AddBoleanInput(
           int index,
           ItemViewModel model,
           StringWriter writer)
        {
            Func<bool?, string> booleanToString = value =>
            {
                return value != null && (bool)value ? "checked" : "";
            };
            var booleanField = CheckOrder(index, model.Collection, new string[]
            {
                booleanToString(model.FirstBoolean),
                booleanToString(model.SecondBoolean),
                booleanToString(model.ThirdBoolean)
            });
            booleanField.Name += "Boolean";
            CreateInput(writer, "input", booleanField, "checkbox", "form-check",
                "form-check-input", "form-check-label");
        }

        private static void AddDateInput(
           int index,
           ItemViewModel model,
           StringWriter writer)
        {
            Func<DateTime?, string> dateToString = value =>
            {
                if (value != null)
                {
                    return String.Format("{0:yyyy-MM-dd}", (DateTime)value);
                }
                return "";
            };
            var dateField = CheckOrder(index, model.Collection, new string[]
            {
                dateToString(model.FirstDate),
                dateToString(model.SecondDate),
                dateToString(model.SecondDate),
            });
            dateField.Name += "Date";
            CreateInput(writer, "input", dateField, "date");
        }

        private static void AddHiddenInputs(ItemViewModel model, StringWriter writer)
        {
            CreateInput(writer, "input",
                new ExtraField()
                {
                    Label = "",
                    Name = "CollectionId",
                    Value = model.Collection.Id.ToString()
                }, "hidden");
            CreateInput(writer, "input",
               new ExtraField()
               {
                   Label = "",
                   Name = "Id",
                   Value = model.Id.ToString()
               }, "hidden");
        }

        public static HtmlString AddExtraFields(
            this IHtmlHelper html,
            ItemViewModel model)
        {
            var writer = new StringWriter();
            FieldType[] fieldTypes = new FieldType[]
            {
                model.Collection.FirstFieldType,
                model.Collection.SecondFieldType,
                model.Collection.ThirdFieldType
            };
            for (var i = 0; i < fieldTypes.Length; i++)
            {
                switch (fieldTypes[i])
                {
                    case FieldType.String:
                        AddStringInput(i, model, writer);
                        break;
                    case FieldType.Integer:
                        AddIntegerInput(i, model, writer);
                        break;
                    case FieldType.Markdown:
                        AddMarkdownInput(i, model, writer);
                        break;
                    case FieldType.Boolean:
                        AddBoleanInput(i, model, writer);
                        break;
                    case FieldType.Date:
                        AddDateInput(i, model, writer);
                        break;
                }
            }
            AddHiddenInputs(model, writer);
            return new HtmlString(writer.ToString());
        }
    }
}
