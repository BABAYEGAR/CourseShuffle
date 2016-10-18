using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Courses : TransportObjects
    {
        public long CoursesId { get; set; }
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [DisplayName("Course Code")]
        public int CourseCode { get; set; }
        [DisplayName("Course Unit")]
        public int CreditUnit { get; set; }
        [DisplayName("Level")]
        public long LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }
        [DisplayName("Department")]
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual  Department Department { get; set; }
        public IEnumerable<Contents> Contents { get; set; }
    }
}
