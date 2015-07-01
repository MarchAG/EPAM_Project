using FileCabinet.Controllers;
using FileCabinet.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileCabinet.Tests.Controllers
{
    [TestClass]
    public class NavigationControllerTests
    {
        [TestMethod]
        public void NavigationShow()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            NavigationController controller = new NavigationController(mock.Object);
            PartialViewResult result = controller.Menu() as PartialViewResult;
            Assert.IsNotNull(result);
        }
    }
}
