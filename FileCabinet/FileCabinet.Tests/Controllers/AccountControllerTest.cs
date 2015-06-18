using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileCabinet;
using FileCabinet.Controllers;
using FileCabinet.Repository;
using Moq;
using FileCabinet.Models;

namespace FileCabinet.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void AccountPartOfProfileReturn()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllUsers).Returns(new List<UserProfile>
            {
                new UserProfile { 
                    Username = "A",
                    Email = "aa@aa.a"
                    },
                new UserProfile { 
                    Username = "B",
                    Email = "bb@bb.b"
                }
            });
            AccountController controller = new AccountController(mock.Object);
            ViewResult result = controller.PartOfProfile(new UserProfile{
                Username = "C",
                Email = "cc@cc.c"
            },
            "sfdsfds") as ViewResult;
            Assert.IsNull(result);

        }

        [TestMethod]
        public void AccountDeleteNull()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllUsers).Returns(new List<UserProfile>
            {
                new UserProfile { 
                    UserProfileId = 1,
                    Username = "A",
                    Email = "aa@aa.a"
                    },
                new UserProfile { 
                    UserProfileId = 2,
                    Username = "B",
                    Email = "bb@bb.b"
                }
            });

            mock.Setup(m => m.FindUserById(It.IsAny<int>())).Returns((int x) => mock.Object.GetAllUsers.FirstOrDefault(y => y.UserProfileId == x));
            
            AccountController controller = new AccountController(mock.Object);
            ViewResult result1 = controller.Delete(3) as ViewResult;
            Assert.IsNull(result1);
        }

        [TestMethod]
        public void AccountEdit()
        {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllUsers).Returns(new List<UserProfile>
            {
                new UserProfile { 
                    UserProfileId = 1,
                    Username = "A",
                    Email = "aa@aa.a"
                    },
                new UserProfile { 
                    UserProfileId = 2,
                    Username = "B",
                    Email = "bb@bb.b"
                }
            });

            mock.Setup(m => m.UpdateUser(It.IsAny<UserProfile>()));
            
            AccountController controller = new AccountController(mock.Object);
            RedirectToRouteResult result1 = controller.Edit(new UserProfile
                {
                     Username = "A",
                     UserProfileId = 1,
                     Email = "AaA@Aa.a"
                }) as RedirectToRouteResult;
            Assert.IsNotNull(result1);
        }
    }
}
