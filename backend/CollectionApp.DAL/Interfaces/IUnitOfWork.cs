using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Collection> Collections { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Image> Images { get; }
        IRepository<Item> Items { get; }
        IRepository<Tag> Tags { get; }
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
        Task SaveAsync();
    }
}
