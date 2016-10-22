using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Faculty : TransportObjects, IEnumerable
    {
        public long FacultyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IEnumerable<Department> Department;
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
