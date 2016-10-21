using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
   public  class CourseFactory
    {
        private readonly CourseDataContext _db = new CourseDataContext();

        /// <summary>
        /// Get all courses under a deparment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Courses> GetAllCoursesForADepartment(long id)
        {
            var courses = _db.Courses.Where(n => n.DepartmentId == id);
            var orderedCourses = courses.OrderByDescending(n => n.DateCreated);
            return orderedCourses;
        }
    }
}
