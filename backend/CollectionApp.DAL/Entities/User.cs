using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CollectionApp.DAL.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Collection> Collections { get; set; }
        public ICollection<Item> LikedItems { get; set; }
    }
}
