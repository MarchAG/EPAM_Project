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
using WebMatrix.WebData;
using Ninject;
using FileCabinet.Repository;
using Microsoft.Security.Application;

namespace FileCabinet.Controllers
{
    [Authorize]
    [InitializeSimpleMembershipAttribute]
    public class ArticlesController : Controller
    {
        //private MyDBContext db = new MyDBContext();
        private IRepository repository;
        private int PageSize = 3;
        private static object Lock = new object();

        public ArticlesController(IRepository rep)
        {
            repository = rep;
        }

        [ValidateInput(false)]
        public ActionResult List(string category, string searchString, string tag, int page = 1)
        {
            searchString = Sanitizer.GetSafeHtmlFragment(searchString);

            int typeOfFile = category == "Audio"? 2 : (category == "Video" ? 1 : 0);
            ArticlesViewModel articlesViewModel = new ArticlesViewModel
            {
                Articles = repository.GetAllArticles
                .Where(x => category == null || x.ContentType == (ContentFileType)typeOfFile),
                Info = new PagingInfo
                {
                    CurrentPage = page,
                    PostsPerPage = PageSize,
                    TotalArticles = category == null ? repository.GetAllArticles.Count() : repository.GetAllArticles.Where(x => x.ContentType == (ContentFileType)typeOfFile).Count()
                },
                CurrentCategory = category,
                SearchString = searchString,
                Tag = tag
            };
            if(!String.IsNullOrEmpty(searchString))
            {
                articlesViewModel.Articles = repository.GetAllArticles
                    .Where(x => x.Title.Contains(searchString) || x.User.Username.Contains(searchString));
                    //.Union(articlesViewModel.Articles.Where(x => x.Description.Contains(searchString))));
                articlesViewModel.Info.TotalArticles = articlesViewModel.Articles.Count();
            }
            if(!String.IsNullOrEmpty(tag))
            {
                articlesViewModel.Articles = repository.GetAllArticles
                    .Where(x => x.Tags.Contains(tag));
                articlesViewModel.Info.TotalArticles = articlesViewModel.Articles.Count();

            }
            articlesViewModel.Articles = articlesViewModel.Articles.OrderByDescending(article => article.ArticleId).Skip((page - 1) * PageSize).Take(PageSize);
           
            return View(articlesViewModel);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = repository.FindArticleById((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create( CreateArticleViewModel createArt)
        {
            createArt.Title = Sanitizer.GetSafeHtmlFragment(createArt.Title);
            createArt.Description = Sanitizer.GetSafeHtmlFragment(createArt.Description);

            int type =  -2;
            if (ModelState.IsValid && (type = createArt.GetFileType()) != -1)
            {
                var article = new Article
                {
                    DateOfPublication = DateTime.Now.ToString(),
                    UserProfileId = WebSecurity.CurrentUserId,
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(createArt.ContentFile.FileName),
                    ContentType = (ContentFileType)type,
                    Description = createArt.Description,
                    Title = createArt.Title,
                    Tags = createArt.Tags
                };
                string path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                if (article.FileName != null) 
                    createArt.ContentFile.SaveAs(Path.Combine(path, article.FileName));
                repository.AddArticle(article);
                return RedirectToAction("List");
            }
            return View(createArt);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = repository.FindArticleById((int)id);
            
            if (article == null)
            {
                return HttpNotFound();
            }
            if (article.UserProfileId != WebSecurity.CurrentUserId)
                return RedirectToAction("List");
            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            article.Description = Sanitizer.GetSafeHtmlFragment(article.Description);
            article.Title = Sanitizer.GetSafeHtmlFragment(article.Title);

            if (ModelState.IsValid)
            {
                repository.UpdateArticle(article);
                return RedirectToAction("List");
            }

            return View(article);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = repository.FindArticleById((int)id);
            if (article == null)
            {
                return HttpNotFound();
            }
            if (article.UserProfileId != WebSecurity.CurrentUserId)
                return RedirectToAction("List");
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = repository.FindArticleById(id);
            repository.DeleteArticle(article);
            FileInfo file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/UploadedFiles/" + article.FileName);
            file.Delete();
            return RedirectToRoute(new
            {
                controller = "Account",
                action = "Profile",
                category = "Articles"
            });
        }

        [Authorize]
        public ActionResult Download(int? id)
        {
            var article = repository.GetAllArticles.FirstOrDefault(x => x.ArticleId == id);
            if (id == null || article == null)
                return HttpNotFound();
            string path = "~/UploadedFiles/" + article.FileName;
            var extension = Path.GetExtension(path);
            return File(path, extension, article.FileName);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SetRating(int? postId, int? rating)
        {
            if (postId == null || rating == null)
            {
                //  Error
                return Json(new { success = false, responseText = "Error." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    Mark mark = repository.GetAllMarks.FirstOrDefault(x => x.ArticleId == (int)postId
                                    && x.UserProfileId == WebSecurity.CurrentUserId);
                    if (mark == null)
                    {
                        mark = new Mark
                        {
                            ArticleId = (int)postId,
                            UserProfileId = WebSecurity.CurrentUserId
                        };
                        repository.AddMark(mark);
                    }
                    mark.Value = 6 - (int)rating;
                    repository.UpdateMark(mark);
                    return Json(new
                    {
                        success = true,
                        average = repository.FindArticleById((int)postId)
                        .Marks.Average(x => x.Value)
                    }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    return Json(new { success = false, responseText = "Error." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
