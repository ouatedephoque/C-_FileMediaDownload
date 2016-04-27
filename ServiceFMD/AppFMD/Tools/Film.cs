using System.Runtime.Serialization;

namespace AppFMD
{
    [DataContract]
    public partial class Film
    {
        [DataMember]
        public int FilmId { get; set; }

        [DataMember]
        public string FilmTitle { get; set; }

        [DataMember]
        public string FilmLink { get; set; }

        [DataMember]
        public int FilmPourcent { get; set; }

        [DataMember]
        public string FilmExtension { get; set; }

        [DataMember]
        public string FilmPathPC { get; set; }

        public string FilmProgressionAffichage
        {
            get
            {
                return this.FilmPourcent + "%";
            }
        }
    }
}