using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class TransportObjects
    {
        public virtual long CreatedBy { get; set; }

        public virtual DateTime DateCreated { get; set; }

        public virtual DateTime DateLastModified { get; set; }

        public virtual long LastModifiedBy { get; set; }
    }
}
