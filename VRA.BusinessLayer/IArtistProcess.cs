using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface IArtistProcess
    {
        /// <summary>
        /// Возвращает список Художников
        /// </summary>
        /// <returns>список художников</returns>
        IList<ArtistDto> GetList();

        /// <summary>
        /// Возвращает художника по его ID
        /// </summary>
        /// <param name="id">id художника</param>
        /// <returns>Художник</returns>
        ArtistDto Get(int id);

        /// <summary>
        /// Добавляетхудожника
        /// </summary>
        /// <param name="artist"></param>
        void Add(ArtistDto artist);

        /// <summary>
        /// Обновляет данные о художнике
        /// </summary>
        /// <param name="artist">Художник, изменения которого надо сохранить</param>
        void Update(ArtistDto artist);

        /// <summary>
        /// Удаляет художника
        /// </summary>
        /// <param name="id">id художника, которого надо удалить</param>
        void Delete(int id);

        IList<ArtistDto> SearchArtists(string Name, string Nation);

    }
}
