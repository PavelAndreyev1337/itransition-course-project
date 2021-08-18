using System.Collections.Generic;
using CollectionApp.BLL.Enums;
using Microsoft.AspNetCore.Http;

namespace CollectionApp.WEB.ViewModels
{
    public class CollectionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FirstFieldName { get; set; }
        public FieldType FirstFieldType { get; set; }
        public string SecondFieldName { get; set; }
        public FieldType SecondFieldType { get; set; }
        public string ThirdFieldName { get; set; }
        public FieldType ThirdFieldType { get; set; }
    }
}
