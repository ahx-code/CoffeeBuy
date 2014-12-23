using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CoffeeBuy.Models;

namespace CoffeeBuy.Controllers
{
    public class HesapController : Controller
    {

        private void AlisverisListesiniTasi(string UserName)
        {
            // Associate shopping cart items with logged-in user
            var cart = AlisverisListesi.GetListe(this.HttpContext);

            cart.ListeTasi(UserName);
            Session[AlisverisListesi.listeSessionKey] = UserName;
        }

        //
        // GET: /Hesap/Giris

        public ActionResult Giris()
        {
            return View();
        }

        //
        // POST: /Hesap/Giris

        [HttpPost]
        public ActionResult Giris(GirisModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.KullaniciAdi, model.Sifre))
                    {
                        AlisverisListesiniTasi(model.KullaniciAdi);

                        FormsAuthentication.SetAuthCookie(model.KullaniciAdi, model.BeniHatirla);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "AnaSayfa");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Adınız veya Şifreniz Hatalı.");
                    }
                }

            }
            catch (Exception ex)
            {

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "AnaSayfa");
        }

        //
        // GET: /Hesap/KayitOl

        public ActionResult KayitOl()
        {
            return View();
        }

        //
        // POST: /Hesap/KayitOl

        [HttpPost]
        public ActionResult KayitOl(KayitOlModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.KullaniciAdi, model.Sifre, model.Email, "Soru", "Cevabi", true, null, out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        AlisverisListesiniTasi(model.KullaniciAdi);

                        FormsAuthentication.SetAuthCookie(model.KullaniciAdi, false);
                        return RedirectToAction("Index", "AnaSayfa");
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(createStatus));
                    }
                }   
            }catch(Exception ex){
                ModelState.AddModelError(String.Empty,ex.Message);
            }

                return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult SifreDegistir()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult SifreDegistir(SifreDegistirModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.EskiSifre, model.YeniSifre);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("SifreDegisimBasarili");
                }
                else
                {
                    ModelState.AddModelError("", "Eski Şifre ile Yeni Şifre Aynı Değil.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult SifreDegisimBasarili()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
