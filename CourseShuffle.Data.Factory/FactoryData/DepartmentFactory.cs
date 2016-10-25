using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
   public class DepartmentFactory
    {
        private readonly DepartmentDataContext _db = new DepartmentDataContext();

        /// <summary>
        /// Get all departments under a faculty
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Department> GetAllDepartmentsForAFaculty(long id)
        {
            var departments = _db.Departments.Where(n => n.FacultyId == id);
            var oderedDepartments = departments.OrderByDescending(n => n.DateCreated);
            return oderedDepartments;
        }
        /// <summary>
        /// Get departments for a logged in user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department GetAppUserDepartmet(long id)
        {
            var department = _db.Departments.Where(n => n.DepartmentId == id).FirstOrDefault();
            return department;
        }
    }
}
