using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CW.Models
{
    public class ShutterContext: DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Lifecycle> Lifecycles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}