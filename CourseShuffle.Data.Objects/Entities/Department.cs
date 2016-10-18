using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Department : TransportObjects
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Faculty")]
        public long FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
    }
}
