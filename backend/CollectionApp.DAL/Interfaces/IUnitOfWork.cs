using CollectionApp.DAL.EF;
using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationContext Context { get; }
        IRepository<Collection> Collections { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Image> Images { get; }
        IRepository<Item> Items { get; }
        IRepository<Tag> Tags { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        Task SaveAsync();
    }
}
