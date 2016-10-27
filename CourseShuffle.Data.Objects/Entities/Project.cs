using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Project : TransportObjects
    {
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Category { get; set; }
        public string Document { get; set; }
        public long SessionId { get; set; }
        [ForeignKey("SessionId")]
        public Session Session { get; set; }
        public long DepartmentId { get; set; } 
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } 
    }
}
