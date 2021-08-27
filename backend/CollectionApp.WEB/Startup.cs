using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CollectionApp.DAL.EF;
using Microsoft.EntityFrameworkCore;
using CollectionApp.DAL.Entities;
using CollectionApp.DAL.Interfaces;
using CollectionApp.DAL.Repositories;
using CollectionApp.BLL.Interfaces;
using CollectionApp.BLL.Services;
using Microsoft.AspNetCore.Identity;
using CollectionApp.WEB.Hubs;
using Microsoft.AspNetCore.Http;

namespace CollectionApp.WEB
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("CollectionApp.DAL")));
            services.AddIdentity<User, IdentityRole>(options => 
            {
                options.User.AllowedUserNameCharacters = string.Empty;
            })
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddControllersWithViews();
            services.AddAuthentication()
                .AddCookie()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.SignInScheme = IdentityConstants.ExternalScheme;
                }).AddReddit(redditOptions =>
                {
                    redditOptions.ClientId = Configuration["Authentication:Reddit:ClientId"];
                    redditOptions.ClientSecret = Configuration["Authentication:Reddit:ClientSecret"];
                    redditOptions.SignInScheme = IdentityConstants.ExternalScheme;
                });
            services.AddSignalR();
            services.ConfigureApplicationCookie(options => options.LoginPath = "/");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapHub<MessageHub>("/comments");
            });
        }
    }
}
