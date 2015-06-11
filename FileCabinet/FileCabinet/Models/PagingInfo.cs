using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class PagingInfo
    {
        // Кол-во товаров
        public int TotalArticles { get; set; }
		
		// Кол-во товаров на одной странице
        public int PostsPerPage { get; set; }
		
		// Номер текущей страницы
        public int CurrentPage { get; set; }

		// Общее кол-во страниц
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalArticles / PostsPerPage); }
        }
    }
}