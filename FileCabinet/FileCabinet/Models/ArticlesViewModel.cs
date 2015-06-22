using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class ArticlesViewModel
    {
        public IQueryable<Article> Articles { get; set; }
        public PagingInfo Info { get; set; }
        public string CurrentCategory { get; set; }
        public string SearchString { get; set; }
    }
}