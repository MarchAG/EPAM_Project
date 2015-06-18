using FileCabinet.filters;
using FileCabinet.Models;
using FileCabinet.Repository;
using Microsoft.Security.Application;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace FileCabinet.Controllers
{
    [InitializeSimpleMembershipAttribute]
    public class AccountController : Controller
    {
        private IRepository Repository { get; set; }
        // GET: Account
        public AccountController(IRepository rep)
        {
            Repository = rep;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login, string returnUrl)
        {
            login.Username = Sanitizer.GetSafeHtmlFragment(login.Username);
            login.Password = Sanitizer.GetSafeHtmlFragment(login.Password);
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
            reg.Username = Sanitizer.GetSafeHtmlFragment(reg.Username);
            reg.Password = Sanitizer.GetSafeHtmlFragment(reg.Password);
            reg.EMail = Sanitizer.GetSafeHtmlFragment(reg.EMail);

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
        public ActionResult Profile(string category)
        {
            var user = Repository.GetAllUsers.FirstOrDefault(x => x.Username == User.Identity.Name);
            ViewBag.Categ = category;
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
        public ActionResult PartOfProfile(UserProfile user, string category)
        {
            if (category == "Info")
                return PartialView("_PersonalInfo", user);
            if (category == "Articles")
            {
                IEnumerable<Article> articles = Repository.GetAllArticles;
                if (!Roles.GetRolesForUser(User.Identity.Name).Contains("Admin"))
                    articles = articles.Where(x => x.UserProfileId == WebSecurity.CurrentUserId);
                return PartialView("_PersonalArticles", articles);
            }
            else if (category == "Users" && Roles.GetRolesForUser(User.Identity.Name).Contains("Admin"))
            {
                var users = Repository.GetAllUsers.SkipWhile(x => x.Username == User.Identity.Name);
                return PartialView("_Users", users);
            }
            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserProfileId,Username,Email,DateOfBirth")] UserProfile user)
        {
            user.Username = Sanitizer.GetSafeHtmlFragment(user.Username);
            user.Email = Sanitizer.GetSafeHtmlFragment(user.Email);
            user.DateOfBirth = Sanitizer.GetSafeHtmlFragment(user.DateOfBirth);

            if(ModelState.IsValid)
            {
                Repository.UpdateUser(user);
                return RedirectToAction("Profile");
            }
            return View(user);
        }

        [Authorize(Roles="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile user = Repository.FindUserById((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.UserProfileId == WebSecurity.CurrentUserId)
                return RedirectToAction("Profile");
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = Repository.GetAllUsers.FirstOrDefault(x => x.UserProfileId == id);
            try
            {
                if (Roles.GetRolesForUser(user.Username).Count() > 0)
                {
                    Roles.RemoveUserFromRoles(user.Username, Roles.GetRolesForUser(user.Username));
                }
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.Username); // deletes record from webpages_Membership table
                ((SimpleMembershipProvider)Membership.Provider).DeleteUser(user.Username, true); // deletes record from UserProfile table

                return RedirectToAction("Profile", new { category = "Users" });
            }
            catch
            {
                return View(id);
            }
        }

        protected override void Dispose(bool disposing)
        {
            Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}