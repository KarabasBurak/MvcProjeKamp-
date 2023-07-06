using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampı.Controllers
{
    public class WriterPanelController : Controller
    {
        TitleManager tm = new TitleManager(new EfTitleDal());
        CategoryManager cm=new CategoryManager(new EfCategoryDal());
        Context c = new Context();

        int writerIdInfo;
        public ActionResult WriterProfile()
        {
            return View();
        }


        public ActionResult MyTitle(string p) 
        {  
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = tm.GetListByWriter(writerIdInfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewTitle()
        {
            
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewTitle(Title p)
        {
            string writerMailİnfo = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == writerMailİnfo).Select(y => y.WriterID).FirstOrDefault();
            p.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerIdInfo;
            p.TitleStatus = true;
            tm.TitleAdd(p);
            return RedirectToAction("MyTitle");
        }

        [HttpGet]
        public ActionResult EditTitle(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString(),
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var titleValue = tm.GetByID(id);
            return View(titleValue);
        }

        [HttpPost]
        public ActionResult EditTitle(Title p)
        {
            tm.TitleUpdate(p);
            return RedirectToAction("MyTitle");
        }

        public ActionResult DeleteTitle(int id)
        {
            var titleValue = tm.GetByID(id);
            titleValue.TitleStatus = false;
            tm.TitleDelete(titleValue);
            return RedirectToAction("MyTitle");
        }

        public ActionResult AllTitle()
        {
            var titles = tm.GetList();
            return View(titles);

        }
    }
}