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
        IRepository<Collection, int> Collections { get; }
        IRepository<Comment, int> Comments { get; }
        IRepository<Image, int> Images { get; }
        IRepository<Item, int> Items { get; }
        IRepository<Tag, int> Tags { get; }
        IRepository<User, string> Users { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        Task SaveAsync();
    }
}
