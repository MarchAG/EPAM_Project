using FileCabinet.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            NavigationController controller = new NavigationController();
            PartialViewResult result = controller.Menu() as PartialViewResult;
            Assert.IsNotNull(result);
        }
    }
}
