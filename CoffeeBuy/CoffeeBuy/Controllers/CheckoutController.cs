using System;
using System.Linq;
using System.Web.Mvc;
using CoffeeBuy.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        CoffeeBuyEntities coffeeDB = new CoffeeBuyEntities();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/AdresveOdeme

        public ActionResult AdresveOdeme()
        {
            return View();
        }

        //
        // POST: /Checkout/AdresveOdeme

        [HttpPost]
        public ActionResult AdresveOdeme(FormCollection values)
        {
            var siparis = new Siparis();
            TryUpdateModel(siparis);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(siparis);
                }
                else
                {
                    siparis.KullaniciAdi = User.Identity.Name;
                    siparis.SiparisinVerildigiGun = DateTime.Now;

                    //Siparişi Kaydet
                    coffeeDB.Siparis.Add(siparis);
                    coffeeDB.SaveChanges();

                    //Sipariş İşlemine devam et.
                    var cart = AlisverisListesi.GetListe(this.HttpContext);
                    cart.SiparisOlustur(siparis);

                    return RedirectToAction("Complete",
                        new { id = siparis.SiparisId });
                }

            }
            catch
            {
                //Hatalarıyla Ekranda Göster
                return View(siparis);
            }
        }

        //
        // GET: /Checkout/Bitti

        public ActionResult Bitti(int id)
        {
            // Bu siparişi Sahiplenmiş Kullanıcıyı Göster
            bool isValid = coffeeDB.Siparis.Any(
                o => o.SiparisId == id &&
                o.KullaniciAdi == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
