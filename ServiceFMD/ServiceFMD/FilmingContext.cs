using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServiceFMD
{
    public class FilmingContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
    }
}
