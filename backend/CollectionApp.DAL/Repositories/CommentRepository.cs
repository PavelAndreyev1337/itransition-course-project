using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    public class CommentRepository : EfCoreRepository<Comment, ApplicationContext>
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
