using System.Collections.Generic;

namespace CollectionApp.BLL.Interfaces
{
    public interface ICollectionService
    {
        public IEnumerable<string> GetTopics();
    }
}
