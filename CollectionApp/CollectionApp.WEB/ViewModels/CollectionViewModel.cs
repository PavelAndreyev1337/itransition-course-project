using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Enums;
using Microsoft.AspNetCore.Http;

namespace CollectionApp.WEB.ViewModels
{
    public class CollectionViewModel
    {
        public int? Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Topic { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
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
