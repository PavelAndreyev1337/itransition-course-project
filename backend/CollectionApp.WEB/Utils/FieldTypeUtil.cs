using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using CollectionApp.WEB.ViewModels;
using System;
using System.Collections.Generic;

namespace CollectionApp.WEB.Utils
{
    public static class FieldTypeUtil
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
        private static ExtraField GetStringField(
            int index,
            ItemViewModel model,
            FieldType fieldType)
        {
            var stringField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstString,
                model.SecondString,
                model.ThirdString
            });
            stringField.Name += "String";
            stringField.Element = "input";
            stringField.FieldType = fieldType;
            return stringField;
        }

        private static ExtraField GetIntegerField(
           int index,
           ItemViewModel model,
           FieldType fieldType)
        {
            var integerField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstInteger.ToString(),
                model.SecondInteger.ToString(),
                model.ThirdInteger.ToString()
            });
            integerField.Name += "Integer";
            integerField.Element = "input";
            integerField.ElementType = "number";
            integerField.FieldType = fieldType;
            return integerField;
        }


        private static ExtraField GetMarkdownField(
           int index,
           ItemViewModel model,
           FieldType fieldType)
        {
            var markdownField = CheckOrder(index, model.Collection, new string[]
            {
                model.FirstText,
                model.SecondText,
                model.ThirdText
            });
            markdownField.Name += "Text";
            markdownField.Element = "textarea";
            markdownField.FieldType = fieldType;
            return markdownField;
        }

        private static ExtraField GetBoleanField(
           int index,
           ItemViewModel model,
           FieldType fieldType)
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
            booleanField.Element = "input";
            booleanField.ElementType = "checkbox";
            booleanField.ContainerCssClass = "form-check";
            booleanField.InputCssClass = "form-check-input";
            booleanField.LabelCssClass = "form-check-label";
            booleanField.FieldType = fieldType;
            return booleanField;
        }

        private static ExtraField GetDateField(
           int index,
           ItemViewModel model,
           FieldType fieldType)
        {
            Func<DateTime?, string> dateToString = value =>
            {
                return DateUtil.ConvertDate(value);
            };
            var dateField = CheckOrder(index, model.Collection, new string[]
            {
                dateToString(model.FirstDate),
                dateToString(model.SecondDate),
                dateToString(model.SecondDate),
            });
            dateField.Name += "Date";
            dateField.Element = "input";
            dateField.ElementType = "date";
            dateField.FieldType = fieldType;
            return dateField;
        }

        private static ExtraField GetHiddenCollectionInput(ItemViewModel model)
        {
            return new ExtraField()
            {
                Label = "",
                Name = "CollectionId",
                Value = model.Collection.Id.ToString(),
                Element = "input",
                ElementType = "hidden"
            };
          
        }

        private static ExtraField GetHiddenItemInput(ItemViewModel model)
        {
            return new ExtraField()
            {
                Label = "",
                Name = "Id",
                Value = model.Id.ToString(),
                Element = "input",
                ElementType = "hidden"
            };
        }

        public static IEnumerable<ExtraField> GetFields(
            ItemViewModel model,
            bool getHidden = true)
        {
            FieldType[] fieldTypes = new FieldType[]
            {
                model.Collection.FirstFieldType,
                model.Collection.SecondFieldType,
                model.Collection.ThirdFieldType
            };
            var fields = new List<ExtraField>();
            for (var i = 0; i < fieldTypes.Length; i++)
            {
                var fieldType = fieldTypes[i];
                switch (fieldType)
                {
                    case FieldType.String:
                        fields.Add(GetStringField(i, model, fieldType));
                        break;
                    case FieldType.Integer:
                        fields.Add(GetIntegerField(i, model, fieldType));
                        break;
                    case FieldType.Markdown:
                        fields.Add(GetMarkdownField(i, model, fieldType));
                        break;
                    case FieldType.Boolean:
                        fields.Add(GetBoleanField(i, model, fieldType));
                        break;
                    case FieldType.Date:
                        fields.Add(GetDateField(i, model, fieldType));
                        break;
                }
            }
            if (getHidden)
            {
                fields.Add(GetHiddenItemInput(model));
                fields.Add(GetHiddenCollectionInput(model));
            }
            return fields;
        }
    }
}
