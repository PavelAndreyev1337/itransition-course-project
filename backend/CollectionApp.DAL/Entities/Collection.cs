using CollectionApp.DAL.Attributes;
using CollectionApp.DAL.Enums;
using CollectionApp.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionApp.DAL.Entities
{
    public class Collection : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        [Topic(new string[] { "Alcohol", "Books" })]
        public string Topic { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public User User { get; set; }
        [StringLength(50)]
        public string FirstFieldName { get; set; }
        [StringLength(50)]
        public string SecondFieldName { get; set; }
        [StringLength(50)]
        public string ThirdFieldName { get; set; }
        public FieldType FirstFieldType { get; set; }
        public FieldType SecondFieldType { get; set; }
        public FieldType ThirdFieldType { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
