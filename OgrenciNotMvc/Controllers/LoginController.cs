using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using System.Web.Security;

namespace OgrenciNotMvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbMvcOkulEntities db = new DbMvcOkulEntities();
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TBLOGRETMENLER t)
        {
            var bilgiler = db.TBLOGRETMENLER.FirstOrDefault(x => x.AD == t.AD && x.SIFRE == t.SIFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.AD, false);
                return RedirectToAction("Index", "Ogrenci");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Login");
        }
    }
}