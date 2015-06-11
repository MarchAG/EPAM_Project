using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileCabinet.Models;
using FileCabinet.filters;
using System.IO;
using System.Web.Security;

namespace FileCabinet.Controllers
{
    //[InitializeSimpleMembershipAttribute]
    public class ArticlesController : Controller
    {
        private MyDBContext db = new MyDBContext();
        private int PageSize = 3;
        // GET: Articles
        public ActionResult List(string category, string searchString, int page = 1)
        {
            int typeOfFile = category == "Audio"? 2 : (category == "Video" ? 1 : 0);
            ArticlesViewModel articlesViewModel = new ArticlesViewModel
            {
                Articles = db.Articles
                .Where(x => category == null || x.ContentType == (ContentFileType)typeOfFile),
                Info = new PagingInfo
                {
                    CurrentPage = page,
                    PostsPerPage = PageSize,
                    TotalArticles = category == null ? db.Articles.Count() : db.Articles.Where(x => x.ContentType == (ContentFileType)typeOfFile).Count()
                },
                CurrentCategory = category,
                SearchString = searchString
            };
            if(!String.IsNullOrEmpty(searchString))
            {
                articlesViewModel.Articles = db.Articles
                    .Where(x => x.Title.Contains(searchString) || x.User.Username.Contains(searchString));
                    //.Union(articlesViewModel.Articles.Where(x => x.Description.Contains(searchString))));
                articlesViewModel.Info.TotalArticles = articlesViewModel.Articles.Count();
            }
            articlesViewModel.Articles = articlesViewModel.Articles.OrderBy(article => article.ArticleId).Skip((page - 1) * PageSize).Take(PageSize);
           
            return View(articlesViewModel);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.UserProfileId = new SelectList(db.Users, "UserProfileId", "Username");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "ArticleId,UserProfileId,Title,PathToContent,ContentType,DateOfPublication")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CreateArt createArt)
        {
            int type =  -2;
            if (ModelState.IsValid && (type = createArt.GetFileType()) != -1)
            {
                var article = new Article();
                article.DateOfPublication = DateTime.Now.ToString();
                article.UserProfileId = db.Users.First(x => x.Username == User.Identity.Name).UserProfileId;
                article.FileName = Guid.NewGuid().ToString() + Path.GetExtension(createArt.ContentFile.FileName);
                article.ContentType = (ContentFileType)type;
                article.Description = createArt.Description;
                article.Title = createArt.Title;
                string path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                if (article.FileName != null) createArt.ContentFile.SaveAs(Path.Combine(path, article.FileName));
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserProfileId = new SelectList(db.Users, "UserProfileId", "Username", article.UserProfileId);
            return View(createArt);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserProfileId = new SelectList(db.Users, "UserProfileId", "Username", article.UserProfileId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "ArticleId,UserProfileId,Title,FileName,ContentType,DateOfPublication, Description, User")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserProfileId = new SelectList(db.Users, "UserProfileId", "Username", article.UserProfileId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/UploadedFiles/" + article.FileName);
            file.Delete();
            return RedirectToAction("Index");
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
