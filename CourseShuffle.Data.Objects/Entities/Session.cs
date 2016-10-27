using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Session : TransportObjects
    {
        public long SessionId { get; set; }
        public long Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  virtual  IEnumerable<Project> Projects { get; set; }

    }
}
