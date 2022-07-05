using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        [Authorize]
        public ActionResult Index()
        {
            var ogrenciler = db.TBLOGRENCILER.ToList();
            return View(ogrenciler);
        }
        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            List<SelectListItem> deger = (from i in db.TBLKULUPLER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KULUPAD,
                                              Value = i.KULUPID.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;
            
            return View();

        }
        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCILER p3)
        {
            var kp = db.TBLKULUPLER.Where(m => m.KULUPID == p3.TBLKULUPLER.KULUPID).FirstOrDefault();
            p3.TBLKULUPLER = kp;

           
            db.TBLOGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var og = db.TBLOGRENCILER.Find(id);
            db.TBLOGRENCILER.Remove(og);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OGetir(int id)
        {
            var og = db.TBLOGRENCILER.Find(id);
            return View("OGetir", og);
        }
        public ActionResult Guncelle(TBLOGRENCILER p)
        {
            var Ogr = db.TBLOGRENCILER.Find(p.OGRENCIID);
            Ogr.OGRAD = p.OGRAD;
            Ogr.OGRSOYAD = p.OGRSOYAD;
            Ogr.OGRFOTO = p.OGRFOTO;
            Ogr.OGRCINSIYET = p.OGRCINSIYET;
            Ogr.OGRKULUP = p.OGRKULUP;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}