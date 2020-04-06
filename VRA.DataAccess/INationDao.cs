using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface INationDao
    {
        /// <summary>
        /// Загрузить все национальности
        /// </summary>
        /// <returns></returns>
        IList<Nation> Load();
        /// <summary>
        /// Получить национальность по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Nation Get(int id);
        void Add(Nation nation);
        void Delete(int id);
    }
}
