using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Level
    {
        public long LevelId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
    }
}
