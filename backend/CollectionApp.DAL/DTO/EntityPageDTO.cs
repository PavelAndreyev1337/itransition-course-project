using System.Collections.Generic;

namespace CollectionApp.DAL.DTO
{
    public class EntityPageDTO<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public Page Page { get; set; }
    }
}
