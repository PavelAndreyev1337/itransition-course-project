using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    class ImageRepository : EfCoreRepository<Image, ApplicationContext>
    {
        public ImageRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
