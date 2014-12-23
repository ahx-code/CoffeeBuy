using System.ComponentModel.DataAnnotations;
namespace CoffeeBuy.Models
{
    public class SiparisDetaylari
    {
        [Key]
        public int SiparisDetayId { get; set; }
        public int SiparisId { get; set; }
        public int KahveId { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }

        public virtual Kahve Kahve { get; set; }
        public virtual Siparis Siparis { get; set; }
    }
}
