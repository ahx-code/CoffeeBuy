using System.ComponentModel.DataAnnotations;

namespace CoffeeBuy.Models
{
    public class Liste
    {
        [Key]
        public int KayitId { get; set; }
        public string ListeId { get; set; }
        public int KahveId { get; set; }
        public int Adet { get; set; }
        public System.DateTime OlusturulduguGun { get; set; }

        public virtual Kahve Kahve { get; set; }
    }
}