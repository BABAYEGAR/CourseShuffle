using System;
using System.Collections.Generic;
using System.Linq;
using CoureShuffle.Data.DataContext.DataContext;
using CourseShuffle.Data.Objects.Entities;

namespace CourseShuffle.Data.Factory.FactoryData
{
    public class LevelFactory
    {
        private readonly LevelDataContext _db = new LevelDataContext();

        /// <summary>
        ///     This method retrives all levels from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Level> GetAllLevels()
        {
            var levels = _db.Levels;
            return levels;
        }
    }
}