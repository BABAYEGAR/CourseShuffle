using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Contents:TransportObjects
    {
        public long ContentsId { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Content Type")]
        public string ContentType { get; set; }
        [Required]
        public string Author { get; set; }
        public string File { get; set; }
        public string LinkText { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [DisplayName("Course")]
        public long CoursesId { get; set; }
        [ForeignKey("CoursesId")]
        public virtual Courses Courses { get; set; }
    }
}
