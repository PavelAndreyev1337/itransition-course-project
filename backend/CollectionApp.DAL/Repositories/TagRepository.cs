using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    class TagRepository : EfCoreRepository<Tag, ApplicationContext>
    {
        public TagRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
