using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeeBuy.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UrunYonetimiController : Controller
    {
        private CoffeeBuyEntities db = new CoffeeBuyEntities();

        //
        // GET: /UrunYonetimi/

        public ViewResult Index()
        {
            var urunler = db.Kahve.Include(kahve => kahve.Cesit).Include(firma => firma.Firma);
            return View(urunler.ToList());
        }

        //
        // GET: /UrunYonetimi/Detay/5

        public ViewResult Detay(int id)
        {
            Kahve kahve = db.Kahve.Find(id);
            return View(kahve);
        }

        //
        // GET: /UrunYonetimi/Olustur

        public ActionResult Olustur()
        {
            ViewBag.CesitId = new SelectList(db.Cesit, "CesitId", "Ad");
            ViewBag.FirmaId = new SelectList(db.Firma, "FirmaId", "Ad");
            return View();
        } 

        //
        // POST: /UrunYonetimi/Olustur

        [HttpPost]
        public ActionResult Olustur(Kahve kahve)
        {
            if (ModelState.IsValid)
            {
                db.Kahve.Add(kahve);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CesitId = new SelectList(db.Cesit, "CesitId", "Ad", kahve.CesitId);
            ViewBag.FirmaId = new SelectList(db.Firma, "FirmaId", "Ad", kahve.FirmaId);
            return View(kahve);
        }
        
        //
        // GET: /UrunYonetimi/Duzenle/5
 
        public ActionResult Duzenle(int id)
        {
            Kahve kahve = db.Kahve.Find(id);
            ViewBag.CesitId = new SelectList(db.Cesit, "CesitId", "Ad", kahve.CesitId);
            ViewBag.FirmaId = new SelectList(db.Firma, "FirmaId", "Ad", kahve.FirmaId);
            return View(kahve);
        }

        //
        // POST: /UrunYonetimi/Duzenle/5

        [HttpPost]
        public ActionResult Duzenle(Kahve kahve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kahve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CesitId = new SelectList(db.Cesit, "CesitId", "Ad", kahve.CesitId);
            ViewBag.FirmaId = new SelectList(db.Firma, "FirmaId", "Ad", kahve.FirmaId);
            return View(kahve);
        }

        //
        // GET: /UrunYonetimi/Sil/5
 
        public ActionResult Sil(int id)
        {
            Kahve kahve = db.Kahve.Find(id);
            return View(kahve);
        }

        //
        // POST: /UrunYonetimi/Sil/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Kahve kahve = db.Kahve.Find(id);
            db.Kahve.Remove(kahve);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}