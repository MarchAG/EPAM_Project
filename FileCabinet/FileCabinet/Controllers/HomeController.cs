using FileCabinet.filters;
using FileCabinet.Models;
using FileCabinet.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace FileCabinet.Controllers
{

    public class HomeController : Controller
    {

        public ActionResult Index(int? id)
        {
            return View();
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
            base.Dispose(disposing);
        }
    }
}