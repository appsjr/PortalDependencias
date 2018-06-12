using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pendencias.Models
{
    public class DTOPendenciaContext : DbContext
    {

        public DTOPendenciaContext() : base("name=DBPendencias")
        {

        }

        // Add a DbSet for each one of your Entities
        public DbSet<DTOPendencia> DTOPendencias { get; set; }

        public DbSet<DTOCategoria> DTOCategorias { get; set; }
    }
}