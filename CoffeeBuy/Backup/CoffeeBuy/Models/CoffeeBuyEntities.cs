using System.Data.Entity;

namespace CoffeeBuy.Models
{
    public class CoffeeBuyEntities : DbContext
    {
        public DbSet<Kahve> Kahve { get; set; }
        public DbSet<Cesit> Cesit { get; set; }
        public DbSet<Firma> Firma { get; set; }
        public DbSet<Liste> Liste { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<SiparisDetaylari> SiparisDetaylari { get; set; }
    }
}