using FileCabinet.filters;
using FileCabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace FileCabinet.Controllers
{
    //[InitializeSimpleMembershipAttribute]
    public class HomeController : Controller
    {
        MyDBContext uc = new MyDBContext();
        public ActionResult Index(int? id)
        {
            if(id == null)
                return View(uc.Articles.ToList());
            return View(uc.Articles.Where(x => x.ArticleId == id).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Content(Article article)
        {
            if(article.ContentType == ContentFileType.Audio)
                return PartialView("AudioContent", article);
            if (article.ContentType == ContentFileType.Video)
                return PartialView("VideoContent", article);
            return PartialView("TextContent", article);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}