using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebBlog.Models;

namespace WebBlog.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
    }
}
