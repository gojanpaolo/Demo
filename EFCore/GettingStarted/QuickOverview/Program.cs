using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace QuickOverview
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new BloggingContext())
            {
                context.Database.EnsureCreated();
            }

            using (var context = new BloggingContext())
            {
                var blog = new Blog { Url = "http://sample.com" };
                context.Blogs.Add(blog);
                context.SaveChanges();
            }

            using (var context = new BloggingContext())
            {
                var blogs = context.Blogs
                    .ToList();
            }
        }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=db.sqlite");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
