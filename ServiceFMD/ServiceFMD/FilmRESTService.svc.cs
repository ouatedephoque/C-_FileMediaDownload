using ServiceFMD;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceFMD
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "FilmRESTService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez FilmRESTService.svc ou FilmRESTService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class FilmRESTService : IFilmRESTService
    {
        private Dictionary<WebClient, Film> dictionnaryFilmWebClient;

        public List<Film> GetFilmList()
        {
            List<Film> films = new List<Film>();

            using (var ctx = new FMDModel())
            {
                films = ctx.film.ToList();

                foreach(Film f in films)
                {
                    ctx.Entry(f).Reload();
                }
            }

            return films;
        }

        public List<Film> PostAddFilm(Film f)
        {
            List<Film> films = new List<Film>();

            using (var ctx = new FMDModel())
            {
                films = ctx.film.ToList();

                int isExisting = ctx.film.Where(fi => fi.FilmLink.Equals(f.FilmLink)).ToList().Count;

                if(isExisting > 0)
                {
                    Console.WriteLine("Film existant !");
                    return null;
                }
                else
                {
                    ctx.film.Add(f);
                    ctx.SaveChanges();

                    downloadNewFile(f);

                    films = ctx.film.ToList();
                }
                
            }

            return films;
        }

        private void downloadNewFile(Film film)
        {
            if (dictionnaryFilmWebClient == null) dictionnaryFilmWebClient = new Dictionary<WebClient, Film>();

            using (WebClient wc = new WebClient())
            {
                dictionnaryFilmWebClient.Add(wc, film);
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadDataCompleted += Wc_DownloadDataCompleted;
                
                wc.DownloadFileAsync(new System.Uri(film.FilmLink), film.FilmPathPC + "\\"+(film.FilmTitle) +"."+(film.FilmExtension));
            }
        }

        private void Wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var webclient = (WebClient)sender;
            Film f = dictionnaryFilmWebClient[webclient];

            using (var ctx = new FMDModel())
            {
                Film filmDB = ctx.film.First(film => film.FilmLink.Equals(f.FilmLink) && film.FilmTitle.Equals(f.FilmTitle));
                ctx.Entry(filmDB).State = System.Data.Entity.EntityState.Modified;
                filmDB.FilmPourcent = 100;
                ctx.SaveChanges();
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var webclient = (WebClient)sender;
            Film f = dictionnaryFilmWebClient[webclient];

            using (var ctx = new FMDModel())
            {
                Film filmDB = ctx.film.First(film => film.FilmLink.Equals(f.FilmLink) && film.FilmTitle.Equals(f.FilmTitle));

                if (filmDB.FilmPourcent < e.ProgressPercentage)
                {
                    filmDB.FilmPourcent = e.ProgressPercentage;
                    ctx.Entry(filmDB).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }
    }
}
