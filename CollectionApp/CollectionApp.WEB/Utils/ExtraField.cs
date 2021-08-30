using CollectionApp.DAL.Enums;

namespace CollectionApp.WEB.Utils
{
    public class ExtraField
    {
        public string ContainerCssClass { get; set; } = "mb-3";
        public string Label { get; set; }
        public string LabelCssClass { get; set; } = "form-label";
        public string Name { get; set; }
        public string Element { get; set; }
        public string ElementType { get; set; }
        public string InputCssClass { get; set; } = "form-control";
        public string Value { get; set; }
        public FieldType FieldType { get; set; }
    }
}
