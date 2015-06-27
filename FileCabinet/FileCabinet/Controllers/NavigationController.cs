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
            StringBuilder allTags = new StringBuilder();
            foreach(var item in articles)
            {
                if(!String.IsNullOrEmpty(item.Tags))
                    allTags.Append(item.Tags + " ");
            }
            var tags = allTags.ToString().Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries).Distinct();
            return PartialView("_TagsMenu", tags);
        }
    }
}