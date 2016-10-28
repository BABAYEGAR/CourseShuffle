using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Category :TransportObjects
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public long DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual  Department Department { get; set; }
        public   IEnumerable<Project> Projects { get; set; }
    }
}
