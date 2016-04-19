using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceFMD
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IFilmRESTService" à la fois dans le code et le fichier de configuration.
    [ServiceContract] 
    public interface IFilmRESTService 
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFilmList/")]
        List<Film> GetFilmList();
        
        [OperationContract(Name = "PostAddFilm")]
        [WebInvoke(Method = "POST", UriTemplate = "PostAddFilm/New")]
        List<Film> PostAddFilm(Film film);
    }
}
