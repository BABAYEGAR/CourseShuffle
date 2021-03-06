﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Department : TransportObjects
    {
        [Key]
        public long DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Faculty")]
        public long FacultyId { get; set; }
        [ForeignKey("FacultyId")]
        public virtual Faculty Faculty { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string SubscriptionType { get; set; }
    }
}
