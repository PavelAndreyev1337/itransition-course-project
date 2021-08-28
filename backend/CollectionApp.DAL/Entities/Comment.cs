using CollectionApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionApp.DAL.Entities
{
    public class Comment : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public User User { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
