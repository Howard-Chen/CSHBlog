using CSHBlog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSHBlog.Web.Data
{

    public class CSHDbContext : DbContext
    {
        // public CSHDbContext(DbContextOptions options) : base(options)
        public CSHDbContext(DbContextOptions<CSHDbContext> options) : base(options)
        {


        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }


    }
}
