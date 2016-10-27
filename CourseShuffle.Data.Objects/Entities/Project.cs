using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Project : TransportObjects
    {
        [Key]
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public string Document { get; set; }
        public long SessionId { get; set; }
        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}
