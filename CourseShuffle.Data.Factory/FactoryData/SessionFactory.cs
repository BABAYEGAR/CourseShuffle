using System;
using System.Linq;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
    public class SessionFactory
    {
        private readonly SessionDataContext _db = new SessionDataContext();


        /// <summary>
        ///     This method retrives a session by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Session GetSessionById(int id)
        {
            var session = _db.Sessions.Find(id);
            return session;
        }
    }
}