namespace ServiceFMD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract]
    [Table("bd_fmd.film")]
    public partial class Film
    {
        [DataMember]
        public int FilmId { get; set; }

        [DataMember]
        [Required]
        [StringLength(100)]
        public string FilmTitle { get; set; }

        [DataMember]
        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string FilmLink { get; set; }

        [DataMember]
        public int FilmPourcent { get; set; }

        [DataMember]
        [Required]
        [StringLength(4)]
        public string FilmExtension { get; set; }

        [DataMember]
        [Required]
        [StringLength(255)]
        public string FilmPathPC { get; set; }
    }
}
