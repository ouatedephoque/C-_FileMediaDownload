using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFMD
{
    public class FilmFMDContext : DbContext
    {
        public FilmFMDContext() : base("FilmFMDContext")
        {

        }

        public DbSet<Film> FilmsFMD { get; set; }
    }
}
