using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    public class UserRepository : EfCoreRepository<User, ApplicationContext, string>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
