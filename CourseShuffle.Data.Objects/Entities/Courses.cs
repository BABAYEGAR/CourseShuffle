﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseShuffle.Data.Objects.Entities
{
    public class Courses
    {
        public long ContentId { get; set; }
        public string CourseName { get; set; }
        public int CourseCode { get; set; }
        public int CreditUnit { get; set; }
        public IEnumerable<Level> Level { get; set; }
    }
}
