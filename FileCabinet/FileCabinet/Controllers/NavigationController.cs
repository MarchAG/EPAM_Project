using FileCabinet.Models;
using FileCabinet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileCabinet.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.Selected = category;
            IEnumerable<string> types = Enum.GetNames(typeof(ContentFileType)).OrderBy(x => x);
            return PartialView("_ArticlesMenu", types);
        }
    }
}