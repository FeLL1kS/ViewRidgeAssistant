using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.BusinessLayer
{
    public class ProcessFactory
    {
        /// <summary>
        /// Возвращает объект, реализующий <seealso cref="IArtistProcess"/>
        /// </summary>
        /// <returns></returns>
        public static IArtistProcess GetArtistProcess()
        {
            return new ArtistProcessDb();
        }
        public static ISettingsProcess GetSettingsProcess()
        {
            return new SettingsProcess();
        }
        public static INationProcess GetNationProcess()
        {
            return new NationProcess();
        }
    }
}
