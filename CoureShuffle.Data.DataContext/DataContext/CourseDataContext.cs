using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CourseDataContext : DbContext
    {
        public CourseDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public System.Data.Entity.DbSet<CourseShuffle.Data.Objects.Entities.Courses> Courses { get; set; }

        public System.Data.Entity.DbSet<CourseShuffle.Data.Objects.Entities.Level> Levels { get; set; }
    }
}
