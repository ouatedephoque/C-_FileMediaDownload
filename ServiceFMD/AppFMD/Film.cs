using System.Runtime.Serialization;

namespace AppFMD
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

        public string FilmProgressionAffichage
        {
            get
            {
                return this.FilmProgression + "%";
            }
        }
    }
}