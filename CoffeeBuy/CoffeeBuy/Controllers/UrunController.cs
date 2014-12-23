using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeBuy.Models;

namespace MvcMusicStore.Controllers
{
    public class UrunController : Controller
    {
        CoffeeBuyEntities coffeeDB = new CoffeeBuyEntities();

        //
        // GET: /Urun/

        public ActionResult Index()
        {
            var genres = coffeeDB.Cesit.ToList();

            return View(genres);
        }

        //
        // GET: /Urun/Arastir?cesit=Espresso

        public ActionResult Arastir(string cesit)
        {
            // Cesit ve ilgili Kahve yi Veritabanından Getir.
            var cesitModel = coffeeDB.Cesit.Include("Kahve")
                .Single(g => g.Ad == cesit);

            return View(cesitModel);
        }

        //
        // GET: /Urun/Detay/5

        public ActionResult Detay(int id)
        {
            var album = coffeeDB.Kahve.Find(id);

            return View(album);
        }

        //
        // GET: /Urun/CesitMenu

        [ChildActionOnly]
        public ActionResult CesitMenu()
        {
            var cesitler = coffeeDB.Cesit.ToList();

            return PartialView(cesitler);
        }

    }
}