using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LevelDataContext : DbContext
    {
        public LevelDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public System.Data.Entity.DbSet<CourseShuffle.Data.Objects.Entities.Level> Levels { get; set; }
    }
}
