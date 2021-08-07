using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionApp.DAL.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public ICollection<Item> Items { get; set; }
    }
}
