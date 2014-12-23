using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CoffeeBuy.Models
{
    [Bind(Exclude = "SiparisId")]
    public partial class Siparis
    {
        [ScaffoldColumn(false)]
        public int SiparisId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime SiparisinVerildigiGun { get; set; }

        [ScaffoldColumn(false)]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Adınızı Yazınız")]
        [DisplayName("Ad")]
        [StringLength(160)]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyadı Bilgisi Gereklidir")]
        [DisplayName("Soyad")]
        [StringLength(160)]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Adres Bilgisi Gereklidir")]
        [StringLength(70)]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Şehir Bilgisi Gereklidir")]
        [StringLength(40)]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Ulke Bilgisi Gereklidir")]
        [StringLength(40)]
        public string Ulke { get; set; }

        [Required(ErrorMessage = "Posta Kodu Bilgisi Gereklidir")]
        [DisplayName("Posta Kodu")]
        [StringLength(10)]
        public string PostaKodu { get; set; }

        [Required(ErrorMessage = "Telefon Bilgisi Gereklidir")]
        [StringLength(24)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email Bilgisi Gereklidir")]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email geçerli değil.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<SiparisDetaylari> SiparisDetaylari { get; set; }
    }
}
