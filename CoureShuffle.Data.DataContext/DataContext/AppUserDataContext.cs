using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppUserDataContext : DbContext
    {
        public AppUserDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
