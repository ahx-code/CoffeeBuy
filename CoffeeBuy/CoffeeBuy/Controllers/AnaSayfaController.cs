using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoffeeBuy.Models;
using System;

namespace MvcMusicStore.Controllers
{
    public class AnaSayfaController : Controller
    {
        //
        // GET: /Home/

        CoffeeBuyEntities coffeeDB = new CoffeeBuyEntities();

        public ActionResult Index()
        {
            // En Populer 5 Kahve yi Ekranda Göster
            var albums = GetTopSellingKahves(8);

            return View(albums);
        }

        private List<Kahve> GetTopSellingKahves(int count)
        {
            // Sipariş Detaylarını Albume göre Grupla
            try
            {
                return coffeeDB.Kahve
                    .OrderByDescending(kahve => kahve.SiparisDetaylari.Count())
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty,ex.Message);
                return null;
            }
        }
    }
}