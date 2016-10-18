using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Contents:TransportObjects
    {
        public long ContentId { get; set; }
        public string Name { get; set; }
        public long CourseId { get; set; }
        public virtual Courses Courses { get; set; }
    }
}
