namespace ServiceFMD
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FMDModel : DbContext
    {
        public FMDModel()
            : base("name=FMDContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Film> film { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
                .Property(e => e.FilmTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.FilmLink)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.FilmExtension)
                .IsUnicode(false);
        }
    }
}
