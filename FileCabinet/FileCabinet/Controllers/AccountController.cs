using FileCabinet.Models;
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
        private MyDBContext db = new MyDBContext();
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
                    ModelState.AddModelError("", "Такой ник уже существует");
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
            var user = db.Users.First(x => x.Username == User.Identity.Name);
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            var user = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserProfileId,Username,Email,DateOfBirth")] UserProfile user)
        {
            if(ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(user);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}