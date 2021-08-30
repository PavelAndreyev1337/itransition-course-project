using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CollectionApp.BLL.DTO
{
    public class CollectionDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Topic { get; set; }
        public List<IFormFile> Files { get; set; }
        public string FirstFieldName { get; set; }
        public FieldType FirstFieldType { get; set; }
        public string SecondFieldName { get; set; }
        public FieldType SecondFieldType { get; set; }
        public string ThirdFieldName { get; set; }
        public FieldType ThirdFieldType { get; set; }
        public User User { get; set; }
        public IEnumerable<string> Topics { get; set; }
    }
}
