using CollectionApp.BLL.Interfaces;
using CollectionApp.DAL.Attributes;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace CollectionApp.BLL.Services
{
    public class CollectionService : ICollectionService
    {
        IUnitOfWork Database { get; set; }

        public CollectionService(IUnitOfWork database)
        {
            Database = database;
        }

        public IEnumerable<string> GetTopics()
        {
            var topics = new List<string>();
            var props = typeof(Collection).GetProperties();
            foreach (var prop in props)
            {
                var attrs = (TopicAttribute[]) prop.GetCustomAttributes(typeof(TopicAttribute), false);
                foreach (var attr in attrs)
                {
                    if (attr != null)
                    {
                        topics.AddRange(attr.Topics.ToList<string>());
                    }
                }
            }
            return topics;
        }
    }
}
