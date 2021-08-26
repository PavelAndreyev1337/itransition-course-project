using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CollectionApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
        public ICollection<Item> LikedItems { get; set; } = new List<Item>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
