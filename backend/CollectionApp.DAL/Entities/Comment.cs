﻿using CollectionApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollectionApp.DAL.Entities
{
    public class Comment : IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
