using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FacultyDataContext : DbContext
    {
        public FacultyDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
