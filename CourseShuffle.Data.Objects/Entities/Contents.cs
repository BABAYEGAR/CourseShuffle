using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Contents:TransportObjects
    {
        public long ContentId { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Author { get; set; }
        public DateTime Year { get; set; }
        [DisplayName("Course")]
        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Courses Courses { get; set; }
    }
}
