using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceFMD
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "FilmRESTService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez FilmRESTService.svc ou FilmRESTService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class FilmRESTService : IFilmRESTService
    {

        public List<Film> GetFilmList()
        {
            return Films.Instance.FilmList;
        }
    }
}
