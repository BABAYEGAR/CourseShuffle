using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
   public class ContentFactory
    {
        public ContentDataContext _db = new ContentDataContext();
        /// <summary>
        /// THis method returns all course contents by a specified type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contents> GetCourseContentsByContentType(long courseId,string contentType)
        {
            var allCourseContents = _db.Contents;
            var courseContents = allCourseContents.Where(n => n.CoursesId == courseId);
            var contentsByType = courseContents.Where(n => n.ContentType == contentType);
            return contentsByType.ToList();
        } 
    }
}
