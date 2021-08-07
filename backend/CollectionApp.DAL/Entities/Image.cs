using System.ComponentModel.DataAnnotations;

namespace CollectionApp.DAL.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
