using CollectionApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionApp.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Image> Images { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
