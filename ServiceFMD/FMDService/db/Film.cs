using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceFMD
{
    [DataContract]
    public class Film
    {
        public Film()
        {

        }

        [DataMember(Name = "FilmID")]
        public int FilmID { get; set; }

        [DataMember(Name = "FilmName")]
        public string FilmName { get; set; }

        [DataMember(Name = "FilmLink")]
        public string FilmLink { get; set; }

        [DataMember(Name = "FilmProgression")]
        public float FilmProgression { get; set; }

        [DataMember(Name = "FilmExtension")]
        public string FilmExtension { get; set; }
    }
}