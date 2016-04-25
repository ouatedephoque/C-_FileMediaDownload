using System;
using System.Collections.Generic;
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
            /*Films mesFilms = Films.Instance;
            
            return mesFilms.GetFilmList;*/

            List<Film> films = new List<Film>();

            /*using (var ctx = new FilmFMDContext())
            {
                films = ctx.FilmsFMD.ToList();
            }*/

            return films;
        }

        public List<Film> PostAddFilm(Film film)
        {
            /*Films mesFilms = Films.Instance;

            mesFilms.AddFilm(film);

            downloadNewFile(film);*/

            List<Film> films = new List<Film>();

            /*using (var ctx = new FilmFMDContext())
            {
                ctx.FilmsFMD.Add(film);
                ctx.SaveChanges();

                downloadNewFile(film);

                films = ctx.FilmsFMD.ToList();
            }*/

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
                wc.DownloadFileAsync(new System.Uri(film.FilmLink), "C:\\Users\\leonardo.distasio\\Videos\\FMD\\"+(film.FilmName)+"."+(film.FilmExtension));
            }
        }

        private void Wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var webclient = (WebClient)sender;
            Film f = dictionnaryFilmWebClient[webclient];

            /*using (var ctx = new FilmFMDContext())
            {
                Film filmDB = ctx.FilmsFMD.Where(film => film.FilmLink.Equals(f.FilmLink) && film.FilmName.Equals(f.FilmName)).ToList().ElementAt(0);
                filmDB.FilmProgression = 100;
                ctx.SaveChanges();
            }*/
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var webclient = (WebClient)sender;
            Film f = dictionnaryFilmWebClient[webclient];

            /*using (var ctx = new FilmFMDContext())
            {
                Film filmDB = ctx.FilmsFMD.Where(film => film.FilmLink.Equals(f.FilmLink) && film.FilmName.Equals(f.FilmName)).ToList().ElementAt(0);
                filmDB.FilmProgression = e.ProgressPercentage;
                ctx.SaveChanges();
                ((IObjectContextAdapter)ctx).ObjectContext.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, filmDB);
            }*/
        }
    }
}
