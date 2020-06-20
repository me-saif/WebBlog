using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebBlog.Data;

namespace WebBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            try
            {
                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any())
                {
                    //Create a Role
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    //Create an Admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com"
                    };
                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    //Add Role to User
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
