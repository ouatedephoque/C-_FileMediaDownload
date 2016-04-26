using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFMD
{
    public class FilmTools
    {
        private static List<String> listExtensionFilm = new List<String> { "avi", "mkv", "mp4" };

        public static String getExtensionFilm(String url)
        {
            return url.Substring(url.Length - 3);
        }

        public static bool isAuthorizedFilm(String url)
        {
            return listExtensionFilm.Contains(getExtensionFilm(url));
        }

        public static String getExtensionFilmOrDefault(String url)
        {
            if(isAuthorizedFilm(url))
            {
                return getExtensionFilm(url);
            }

            return "avi";
        }
    }
}
