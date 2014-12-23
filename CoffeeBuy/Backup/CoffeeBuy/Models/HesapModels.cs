using System.ComponentModel.DataAnnotations;

namespace CoffeeBuy.Models
{

    public class SifreDegistirModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şu anki Sifre")]
        public string EskiSifre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " {0} en az {2} karakter uzunluğunda olmalidir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Sifre")]
        public string YeniSifre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Değişikliklerini Uygula")]
        [Compare("YeniSifre", ErrorMessage = "Sifreler Aynı Degil.")]
        public string SifreOnayla { get; set; }
    }

    public class GirisModel
    {
        [Required]
        [Display(Name = "Kullanici Adi")]
        public string KullaniciAdi { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Sifre { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool BeniHatirla { get; set; }
    }

    public class KayitOlModel
    {
        [Required]
        [Display(Name = "Kullanici Adi")]
        public string KullaniciAdi { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} en az {2} karakter uzunluğunda olmalı.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Sifre { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Sifre Onayla")]
        [Compare("Sifre", ErrorMessage = "Sifreler Aynı Degil.")]
        public string SifreOnayla { get; set; }
    }
}
