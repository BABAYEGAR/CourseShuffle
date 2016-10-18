using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Faculty : TransportObjects
    {
        public long FacultyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Department> Department;
    }
}
