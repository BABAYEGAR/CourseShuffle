using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Level : TransportObjects
    {
        public long LevelId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Courses> Courseses { get; set; }

    }
}
