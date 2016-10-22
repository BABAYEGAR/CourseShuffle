using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
    public class FacultyFactory
    {
        private readonly FacultyDataContext _db = new FacultyDataContext();

        /// <summary>
        ///     This method retrives all faculties from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Faculty> GetAllFaculties()
        {
            var faculties = _db.Faculties;
            return faculties;
        }

    }
}
