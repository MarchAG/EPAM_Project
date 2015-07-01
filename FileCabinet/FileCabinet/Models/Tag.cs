using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Value { get; set; }

        public virtual List<Article> Articles { get; set; }

    }
}