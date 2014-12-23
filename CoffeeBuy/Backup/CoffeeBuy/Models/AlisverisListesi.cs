using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeBuy.Models
{
    public partial class AlisverisListesi
    {
        CoffeeBuyEntities coffeeDB = new CoffeeBuyEntities();

        string AlisverisListesiId { get; set; }

        public const string listeSessionKey = "ListeId";

        public static AlisverisListesi GetListe(HttpContextBase context)
        {
            var listeObje = new AlisverisListesi();
            listeObje.AlisverisListesiId = listeObje.GetListeId(context);
            return listeObje;
        }

        // Alisveris Listesi ne yardımcı olan metod
        public static AlisverisListesi GetListe(Controller controller)
        {
            return GetListe(controller.HttpContext);
        }

        public void ListeyeEkle(Kahve kahve)
        {
            // Listede Seçilene Uyan Tüm Kahveleri Getir
            var listeObje = coffeeDB.Liste.SingleOrDefault(
            c => c.ListeId == AlisverisListesiId
                 && c.KahveId == kahve.KahveId);

            if (listeObje == null)
            {
                //Eğer Hiç Liste Oluştrulmmışsa, yeni liste objesi oluştur.
                listeObje = new Liste
                {
                    KahveId = kahve.KahveId,
                    ListeId = AlisverisListesiId,
                    Adet = 1,
                    OlusturulduguGun = DateTime.Now
                };

                coffeeDB.Liste.Add(listeObje);
            }
            else
            {
                // Eğer Aynı Liste Objesi daha önce Oluşturulmuşsa 1 artır.
                listeObje.Adet++;
            }

            // Değişiklikleri Kaydet.
            coffeeDB.SaveChanges();
        }

        public int ListedenCikar(int id)
        {
            // Listeyi Getir
            var listeItem = coffeeDB.Liste.Single(
                            liste => liste.ListeId == AlisverisListesiId
                                        && liste.KayitId == id);

            int itemCount = 0;

            if (listeItem != null)
            {
                if (listeItem.Adet > 1)
                {
                    listeItem.Adet--;
                    itemCount = listeItem.Adet;
                }
                else
                {
                    coffeeDB.Liste.Remove(listeItem);
                }

                // Değişiklikleri Kaydet
                coffeeDB.SaveChanges();
            }

            return itemCount;
        }

        public void ListeBosalt()
        {
            var listeItems = coffeeDB.Liste.Where(liste => liste.ListeId == AlisverisListesiId);

            foreach (var cartItem in listeItems)
            {
                coffeeDB.Liste.Remove(cartItem);
            }

            // Değişiklikleri Kaydet.
            coffeeDB.SaveChanges();
        }

        public List<Liste> GetListeItems()
        {
            return coffeeDB.Liste.Where(cart => cart.ListeId == AlisverisListesiId).ToList();
        }

        public int GetCount()
        {
            // Her bir madde yi Sistemde Bularak Fiyatlarını Topla
            int? count = (from cartItems in coffeeDB.Liste
                          where cartItems.ListeId == AlisverisListesiId
                          select (int?)cartItems.Adet).Sum();

            // Eğer fiyatları null ise 0 dön 
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Albüm Fiyatı ile Adet i Çarparak
            // liste de bulunan her bir kahve nin
            // toplamını bul.
            decimal? total = (from cartItems in coffeeDB.Liste
                              where cartItems.ListeId == AlisverisListesiId
                              select (int?)cartItems.Adet * cartItems.Kahve.Fiyat).Sum();
            return total ?? decimal.Zero;
        }

        public int SiparisOlustur(Siparis siparis)
        {
            decimal siparisTotal = 0;

            var listeItems = GetListeItems();

            // Her bir Ürün için Sipariş Detayı Oluştur.
            foreach (var item in listeItems)
            {
                var siparisDetaylari = new SiparisDetaylari
                {
                    KahveId = item.KahveId,
                    SiparisId = siparis.SiparisId,
                    BirimFiyat = item.Kahve.Fiyat,
                    Miktar = item.Adet
                };

                // Alışveriş Listesine Toplamı Ekle
                siparisTotal += (item.Adet * item.Kahve.Fiyat);

                coffeeDB.SiparisDetaylari.Add(siparisDetaylari);

            }

            siparis.Total = siparisTotal;

            coffeeDB.SaveChanges();

            ListeBosalt();

            return siparis.SiparisId;
        }

        // HttpContextBase i kullanmamın Sebebi cookies lere ulaşmak için.
        public string GetListeId(HttpContextBase context)
        {
            if (context.Session[listeSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[listeSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // GUID i kullanarak yeni bir id oluştur.
                    Guid tempCartId = Guid.NewGuid();

                    // tempCartId cookie olarak kullanıcıya geri gönder
                    context.Session[listeSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[listeSessionKey].ToString();
        }

        // Kullanıcı Siteye Giriş Yaptığında, Alışveriş Listesini
        // kullanıcı Adının Yanında Göster
        public void ListeTasi(string kullaniciAdi)
        {
            var shoppingCart = coffeeDB.Liste.Where(liste => liste.ListeId == AlisverisListesiId);

            foreach (Liste liste in shoppingCart)
            {
                liste.ListeId = kullaniciAdi;
            }
            coffeeDB.SaveChanges();
        }//end ListeTasi
    }
}