using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    public class CollectionRepository : EfCoreRepository<Collection, ApplicationContext, int>
    {
        public CollectionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
