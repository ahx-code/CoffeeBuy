using System.Linq;
using System.Web.Mvc;
using CoffeeBuy.Models;
using CoffeeBuy.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class AlisverisListesiController : Controller
    {
        CoffeeBuyEntities coffeeDB = new CoffeeBuyEntities();

        //
        // GET: /AlisverisListesi/

        public ActionResult Index()
        {
            var listeObjem = AlisverisListesi.GetListe(this.HttpContext);

            // ViewModel lerimi Oluşturuyorum.
            var viewModel = new AlisverisListesiViewModel
            {
                ListeItems = listeObjem.GetListeItems(),
                ListeTotal = listeObjem.GetTotal()
            };

            // View'a Geri Dön.
            return View(viewModel);
        }

        //
        // GET: /AlisverisListesi/ListeyeEkle/5

        public ActionResult ListeyeEkle(int id)
        {

            // Kahve yi Veritabanından Çekiyorum
            var eklencekKahve = coffeeDB.Kahve
                .Single(kahve => kahve.KahveId == id);

            // AlisverisListesine Ekliyorum
            var liste = AlisverisListesi.GetListe(this.HttpContext);

            liste.ListeyeEkle(eklencekKahve);

            // Daha Fazla Alışveriş İçin AnaSayfa ya Dön
            return RedirectToAction("Index");
        }

        //
        // AJAX: /AlisverisListesi/ListedenCikar/5

        [HttpPost]
        public ActionResult ListedenCikar(int id)
        {
            // Urunu Listeden Sil
            var liste = AlisverisListesi.GetListe(this.HttpContext);

            // Kahve nin Adını Getir.
            string albumName = coffeeDB.Liste
                .Single(item => item.KayitId == id).Kahve.Baslik;

            // Listeden Cikar
            int itemCount = liste.ListedenCikar(id);

            // Ekranda Bilgilendirme Mesajını Göster
            var sonuc = new AlisverisListesindenCikarmaViewModel
            {
                Mesaj = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                ListeTotal = liste.GetTotal(),
                ListeCount = liste.GetCount(),
                ItemCount = itemCount,
                SilinmisId = id
            };

            return Json(sonuc);
        }

        //
        // GET: /AlisverisListesi/AlisverisOzeti

        [ChildActionOnly]
        public ActionResult AlisverisOzeti()
        {
            var cart = AlisverisListesi.GetListe(this.HttpContext);

            ViewData["ListeCount"] = cart.GetCount();

            return PartialView("AlisverisOzeti");
        }
    }
}