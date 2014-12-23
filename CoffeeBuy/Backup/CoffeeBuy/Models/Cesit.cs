using System.Collections.Generic;

namespace CoffeeBuy.Models
{
    public partial class Cesit
    {
        public int CesitId { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public List<Kahve> Kahve { get; set; }
    }
}
