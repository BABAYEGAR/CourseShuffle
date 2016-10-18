using CourseShuffle.Data.Objects.Entities;

namespace CoureShuffle.Data.DataContext.DataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContentDataContext : DbContext
    {
        public ContentDataContext()
            : base("name=CourseShuffle")
        {

        }

        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Courses> Courseses { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
