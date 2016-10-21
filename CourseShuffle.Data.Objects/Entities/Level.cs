using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Level : TransportObjects
    {
        public long LevelId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<Courses> Courseses { get; set; }

    }
}
