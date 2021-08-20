using CollectionApp.DAL.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionApp.DAL.Entities
{
    public class Collection
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
        public bool FirstIntegerFieldVisible { get; set; }
        public bool SecondIntegerFieldVisible { get; set; }
        public bool ThirdIntegerFieldVisible { get; set; }
        public bool FirstStringFieldVisible { get; set; }
        public bool SecondStringFieldVisible { get; set; }
        public bool ThirdStringFieldVisible { get; set; }
        public bool FirstTextFieldVisible { get; set; }
        public bool SecondTextFieldVisible { get; set; }
        public bool ThirdTextFieldVisible { get; set; }
        public bool FirstDateFieldVisible { get; set; }
        public bool SecondDateFieldVisible { get; set; }
        public bool ThirdDateFieldVisible { get; set; }
        public bool FirstBoolVisible { get; set; }
        public bool SecondBoolVisible { get; set; }
        public bool ThirdBoolVisible { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
