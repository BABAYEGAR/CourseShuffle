using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Category :TransportObjects
    {
        public String Name { get; set; }
        public long DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
