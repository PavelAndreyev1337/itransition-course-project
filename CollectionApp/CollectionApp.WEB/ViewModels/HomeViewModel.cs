using CollectionApp.DAL.Entities;
using System.Collections.Generic;

namespace CollectionApp.WEB.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> LastCreatedItems { get; set; }
        public IEnumerable<Collection> LagestNumberItems { get; set; }
    }
}
