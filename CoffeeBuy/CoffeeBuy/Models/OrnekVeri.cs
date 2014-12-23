using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CoffeeBuy.Models
{
    public class OrnekVeri : DropCreateDatabaseAlways<CoffeeBuyEntities>
    {
        protected override void Seed(CoffeeBuyEntities context)
        {
            var cesitler = new List<Cesit>
            {
                new Cesit { Ad = "Espresso" },
                new Cesit { Ad = "Americano" },
                new Cesit { Ad = "Filter" },
                new Cesit { Ad = "Mocha" },
                new Cesit { Ad = "Latte" },
                new Cesit { Ad = "Misto" },
            };

            var firmalar = new List<Firma>
            {
                new Firma { Ad = "Tchibo" },
                new Firma { Ad = "Caribou Coffee" },
                new Firma { Ad = "Gloria Jean's Coffee" },
                new Firma { Ad = "Starbucks" }
            };

            new List<Kahve>
            {
                new Kahve { Baslik = "Brasil Beleza", Cesit = cesitler.Single(cesit => cesit.Ad == "Espresso"), Fiyat = 8.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/brasilBeleza.png" },
                new Kahve { Baslik = "Kraftig", Cesit = cesitler.Single(cesit => cesit.Ad == "Espresso"), Fiyat = 7.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/kraftig.png" },
                new Kahve { Baslik = "Colombia Andino", Cesit = cesitler.Single(cesit => cesit.Ad == "Americano"), Fiyat = 8.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/colombiaAndino.png" },
                new Kahve { Baslik = "India Sirisha", Cesit = cesitler.Single(cesit => cesit.Ad == "Americano"), Fiyat = 8.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/indiaSirisha.png" },
                new Kahve { Baslik = "Entkoffeiniert", Cesit = cesitler.Single(cesit => cesit.Ad == "Filter"), Fiyat = 7.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/Entko.png" },
                new Kahve { Baslik = "Ethiopia Abaya", Cesit = cesitler.Single(cesit => cesit.Ad == "Espresso"), Fiyat = 8.95M, Firma = firmalar.Single(firma => firma.Ad == "Tchibo"), KahveResimUrl = "/Content/Images/abaya.png" },
                new Kahve { Baslik = "Americano", Cesit = cesitler.Single(cesit => cesit.Ad == "Americano"), Fiyat = 10.99M, Firma = firmalar.Single(firma => firma.Ad == "Starbucks"), KahveResimUrl = "/Content/Images/americano.png" },
                new Kahve { Baslik = "Machiato", Cesit = cesitler.Single(cesit => cesit.Ad == "Mocha"), Fiyat = 12.99M, Firma = firmalar.Single(firma => firma.Ad == "Starbucks"), KahveResimUrl = "/Content/Images/machiato.png" },
                new Kahve { Baslik = "Northern Light Mocha", Cesit = cesitler.Single(cesit => cesit.Ad == "Mocha"), Fiyat = 8.99M, Firma = firmalar.Single(firma => firma.Ad == "Caribou Coffee"), KahveResimUrl = "/Content/Images/northernLight.png" },
                new Kahve { Baslik = "Iced Chai Tea Latte", Cesit = cesitler.Single(cesit => cesit.Ad == "Latte"), Fiyat = 12.99M, Firma = firmalar.Single(firma => firma.Ad == "Caribou Coffee"), KahveResimUrl = "/Content/Images/icedChaiTeaLatte.png" },
                new Kahve { Baslik = "Coffee Misto", Cesit = cesitler.Single(cesit => cesit.Ad == "Misto"), Fiyat = 8.99M, Firma = firmalar.Single(firma => firma.Ad == "Starbucks"), KahveResimUrl = "/Content/Images/misto.png" },
            }.ForEach(a => context.Kahve.Add(a));
        }
    }
}