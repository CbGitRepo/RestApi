using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Data
{
    public class ClientDBContext : DbContext
    {
        internal object client;

        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options) { }
        public DbSet<Entities.Client> Clients { get; set; }
       public DbSet<Entities.Commande> Commandes { get; set; }
        public DbSet<Entities.Product> Products { get; set; }


    }
}
