using System;
using System.Collections.Generic;
using System.Linq;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
    public class CategoryFactory
    {
        private readonly CategoryDataContext _db = new CategoryDataContext();


        /// <summary>
        ///     This method retrives a category by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Category> GetDepartmentCategories(int id)
        {
            var categories = _db.Categories.Where(n=>n.DepartmentId == id);
            return categories.ToList();
        }
    }
}