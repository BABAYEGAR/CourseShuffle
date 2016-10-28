using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CategoryDataContext : DbContext
    {
        public CategoryDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Category> Categories  { get; set; }
        public virtual DbSet<Department> Departments  { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
