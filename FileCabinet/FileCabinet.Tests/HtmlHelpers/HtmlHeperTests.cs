using FileCabinet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FileCabinet.HtmlHelpers;
using FileCabinet;
using FileCabinet.Controllers;
using FileCabinet.Repository;
using Moq;


namespace FileCabinet.Tests.HtmlHelpers
{
    [TestClass]
    public class HtmlHeperTests
    {
        [TestMethod]
        public void CanGeneratePageLinks()
        {

            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalArticles = 28,
                PostsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void CanGenerateAverageRait()
        {

            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalArticles = 28,
                PostsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.AverageRating(new Article{
                Marks= new List<Mark>{
                    new Mark{Value = 4},
                    new Mark{Value = 3}},
            }, "");
            Assert.AreEqual(@"<div>|3,5|</div>", result.ToString());
        }
    }
}
