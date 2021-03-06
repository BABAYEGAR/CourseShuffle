﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Session : TransportObjects
    {
        [Key]
        public long SessionId { get; set; }
        public string Name { get; set; }
        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        [Required]
        public DateTime EndDate { get; set; }
        public IEnumerable<Project> Projects { get; set; }

    }
}
