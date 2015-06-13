using FileCabinet.Models;
using FileCabinet.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace FileCabinet.Controllers
{
    public class AccountController : Controller
    {
        [Inject]
        public IRepository Repository { get; set; }
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if (WebSecurity.Login(login.Username, login.Password))
                {
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Проверьте введенные данные");
                    return View(login);
                }
            }
            ModelState.AddModelError("", "Проверьте введенные данные");
            return View(login);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register reg)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(reg.Username, reg.Password, new { Email = reg.EMail });
                    Roles.AddUserToRole(reg.Username, "User");
                    WebSecurity.Login(reg.Username, reg.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch(MembershipCreateUserException exc)
                {
                    ModelState.AddModelError("", "Такой псевдоним уже существует");
                    return View(reg);
                }
                
            }
            ModelState.AddModelError("", "Проверьте введенные данные");
            return View(reg);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Profile()
        {
            var user = Repository.GetAllUsers.FirstOrDefault(x => x.Username == User.Identity.Name);
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            var user = Repository.GetAllUsers.FirstOrDefault(x => x.Username == User.Identity.Name);
            return View(user);
        }

        [ChildActionOnly]
        public PartialViewResult PersonalArticles()
        {
            IEnumerable<Article> articles = Repository.GetAllArticles;
            if(!Roles.GetRolesForUser(User.Identity.Name).Contains("Admin"))
                articles = articles.Where(x => x.UserProfileId == WebSecurity.CurrentUserId);
            return PartialView(articles);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserProfileId,Username,Email,DateOfBirth")] UserProfile user)
        {
            if(ModelState.IsValid)
            {
                Repository.UpdateUser(user);
                return RedirectToAction("Profile");
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}