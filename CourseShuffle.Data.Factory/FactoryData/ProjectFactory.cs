using System;
using System.Linq;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
    public class ProjectFactory
    {
        private readonly ProjectDataContext _db = new ProjectDataContext();


        /// <summary>
        ///     This method retrives a project by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project GetProjectById(int id)
        {
            var project = _db.Projects.Find(id);
            return project;
        }
    }
}