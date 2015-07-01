using FileCabinet.Models;
using FileCabinet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FileCabinet.Controllers
{
    public class NavigationController : Controller
    {
        private IRepository repository;

        public NavigationController(IRepository rep)
        {
            repository = rep;
        }
        // GET: Navigation
        public PartialViewResult Menu()
        {
            IEnumerable<string> types = Enum.GetNames(typeof(ContentFileType)).OrderBy(x => x);
            return PartialView("_ArticlesMenu", types);
        }
        public PartialViewResult TagsMenu()
        {
            var articles = repository.GetAllArticles;
            List<string> tags = new List<string>();
            foreach(var item in articles)
            {
                tags.AddRange(item.Tags.Select(x => x.Value));
            }
            return PartialView("_TagsMenu", tags.Distinct());
        }
    }
}