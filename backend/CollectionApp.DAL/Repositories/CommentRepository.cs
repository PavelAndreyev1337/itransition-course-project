using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;

namespace CollectionApp.DAL.Repositories
{
    public class CommentRepository : EfCoreRepository<Comment, ApplicationContext, int>
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
