using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    class ItemRepository : EfCoreRepository<Item, ApplicationContext>
    {
        public ItemRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
