using FileCabinet.Controllers;
using FileCabinet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FileCabinet;
using FileCabinet.Repository;
using Moq;
using System.Net;
using FileCabinet.HtmlHelpers;

namespace FileCabinet.Tests.Controllers
{
   [TestClass]
    public class ArticlesControllerTest
    {
        [TestMethod]
        public void ArticleDetailsShow()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllArticles).Returns(new List<Article>
            {
                new Article { 
                    ArticleId = 1,
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.1", 
                    Description="SomeText", 
                    FileName="1.txt",Title="tit1",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
                new Article { 
                    ArticleId = 2,
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.31", 
                    Description="SomeText1", 
                    FileName="2.txt",Title="tit1",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
            }.AsQueryable());
            mock.Setup(m => m.FindArticleById(It.IsAny<int>())).Returns((int x) => mock.Object.GetAllArticles.FirstOrDefault(y => y.ArticleId == x ));
            ArticlesController controller = new ArticlesController(mock.Object);
            ViewResult result1 = controller.Details(null) as ViewResult;
            Assert.AreEqual(null, result1);

            ViewResult viewDet1 = controller.Details(1) as ViewResult;
            Article result2 = (Article)viewDet1.Model;
            Assert.AreEqual("1.1.1", result2.DateOfPublication);
        }


        [TestMethod]
        public void ArticleListSearchCorrect()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllArticles).Returns(new List<Article>
            {
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.1", 
                    Description="SomeText", 
                    FileName="1.txt",Title="tit1",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.31", 
                    Description="SomeText1", 
                    FileName="2.txt",Title="tit1",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
            }.AsQueryable());
            // Arrange
            ArticlesController controller = new ArticlesController(mock.Object);
            ViewResult viewRes = controller.List(null, "<br/>", null, 1) as ViewResult;
            ArticlesViewModel result = (ArticlesViewModel)viewRes.Model;
            // Assert
            Assert.AreEqual(0, result.Articles.Count());
        }

        [TestMethod]
        public void CanPaginate()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllArticles).Returns(new List<Article>
            {
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.1", 
                    Description="SomeText", 
                    FileName="1.txt",Title="tit1",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "1.1.31", 
                    Description="SomeText1", 
                    FileName="2.txt",Title="tit2",
                    User = new UserProfile
                    {
                        Username = "Anton"
                    }
                },
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "2.1.31", 
                    Description="SomeText4", 
                    FileName="2.txt",Title="tit3",
                    User = new UserProfile
                    {
                        Username = "CCC"
                    }
                },
                new Article { 
                    ContentType = ContentFileType.Text, 
                    DateOfPublication = "3.1.31", 
                    Description="SomeText5", 
                    FileName="5.txt",Title="tit4",
                    User = new UserProfile
                    {
                        Username = "BBB"
                    }
                },
            }.AsQueryable());
            ArticlesController controller = new ArticlesController(mock.Object);

            ViewResult view = controller.List(null, null, null, 2) as ViewResult;
            ArticlesViewModel result = (ArticlesViewModel)view.Model;
            List<Article> articles = result.Articles.ToList();

            Assert.IsTrue(articles.Count == 1);
            Assert.AreEqual(articles[0].Title, "tit4");
        }
    }
}
