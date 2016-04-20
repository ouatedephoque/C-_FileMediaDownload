using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceFMD
{
    public partial class Films
    {
        private static readonly Films _instance = new Films();

        private Films() { }

        public static Films Instance
        {
            get { return _instance; }
        }

        public List<Film> GetFilmList
        {
            get { return films; }
        }

        public void AddFilm(Film film)
        {
            films.Add(film);
        }

        public List<Film> films = new List<Film>()
        {
            new Film() { FilmID=0, FilmName = "Star Wars", FilmLink = "http://google.ch", FilmProgression= 0f },
            new Film() { FilmID=1, FilmName = "Camping", FilmLink = "http://google.ch", FilmProgression= 0f },
            new Film() { FilmID=2, FilmName = "50 Nuances de Grey", FilmLink = "http://google.ch", FilmProgression= 0f }
        };
    }
}