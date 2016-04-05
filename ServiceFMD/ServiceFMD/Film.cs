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
        [DataMember]
        public int FilmID { get; set; }

        [DataMember]
        public string FilmName { get; set; }

        [DataMember]
        public string FilmLink { get; set; }

        [DataMember]
        public float FilmProgression { get; set; }
    }
}