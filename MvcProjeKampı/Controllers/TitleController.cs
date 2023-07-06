using BusinessLayer.Concrete;
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
    public class TitleController : Controller
    {
        TitleManager tm = new TitleManager(new EfTitleDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm= new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var values = tm.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddTitle()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString(),
                                                  }).ToList();
            List<SelectListItem> valueWriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " + x.WriterSurname,
                                                    Value = x.WriterID.ToString()
                                                }).ToList();
            ViewBag.vlc = valuecategory;
            ViewBag.vlw = valueWriter;
            return View();
        }
        [HttpPost]
        public ActionResult AddTitle(Title title)
        {
            title.TitleDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            tm.TitleAdd(title);
            return RedirectToAction("Index");

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
            return RedirectToAction("Index");
        }
        public ActionResult DeleteTitle(int id)
        {
            var titleValue=tm.GetByID(id);
            titleValue.TitleStatus = false;
            tm.TitleDelete(titleValue);
            return RedirectToAction("Index");
        }
        
    }
}