using System.Collections.Generic;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Level : TransportObjects
    {
        public long LevelId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Courses> Courseses { get; set; }

    }
}
