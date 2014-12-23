using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CoffeeBuy.Models
{
    [Bind(Exclude = "AlbumId")]
    public class Kahve
    {
        [ScaffoldColumn(false)]
        public int KahveId { get; set; }

        [DisplayName("Çesit")]
        public int CesitId { get; set; }

        [DisplayName("Firma")]
        public int FirmaId { get; set; }

        [Required(ErrorMessage = "Kahve Başlığını Yazmanız Gerekmektedir")]
        [StringLength(160)]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Kahvenin Fiyatını Yazmanız Gerekmektedir")]
        [Range(0.01, 100.00,
            ErrorMessage = "Fiyat Aralığı 0.01 ve 100.00 aralığında olmalıdır")]
        public decimal Fiyat { get; set; }

        [DisplayName("Kahve Resim URL")]
        [StringLength(1024)]
        public string KahveResimUrl { get; set; }

        public virtual Cesit Cesit { get; set; }
        public virtual Firma Firma { get; set; }
        public virtual List<SiparisDetaylari> SiparisDetaylari { get; set; }
    }
}