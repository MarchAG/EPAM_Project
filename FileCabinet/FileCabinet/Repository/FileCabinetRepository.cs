using FileCabinet.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileCabinet.Repository
{
    public class FileCabinetRepository : IRepository
    {
        [Inject]
        public MyDbContext context { get; set; }

        public IQueryable<UserProfile> GetAllUsers
        {
            get { return context.Users; }
        }

        public void AddUser(UserProfile entity)
        {
            context.Users.Add(entity);
            SaveChanges();
        }

        public void DeleteUser(UserProfile entity)
        {
            DeleteRangeMark(entity.Marks);
            context.Users.Remove(entity);
            SaveChanges();
        }

        public void UpdateUser(UserProfile entity)
        {
            context.Entry(entity).State = EntityState.Modified; 
            SaveChanges();
        }

        public UserProfile FindUserById(int id)
        {
            return context.Users.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public IQueryable<Article> GetAllArticles
        {
            get { return context.Articles; }
        }

        public void AddArticle(Article entity)
        {
            context.Articles.Add(entity);
            SaveChanges();
        }

        public void DeleteArticle(Article entity)
        {
            DeleteRangeMark(entity.Marks);
            context.Articles.Remove(entity);
            SaveChanges();
        }

        public void UpdateArticle(Article entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public Article FindArticleById(int id)
        {
            return context.Articles.Find(id);
        }

        public IQueryable<Mark> GetAllMarks
        {
            get { return context.Marks; }
        }

        public void AddMark(Mark entity)
        {
            context.Marks.Add(entity);
            SaveChanges();
        }

        public void DeleteRangeMark(IEnumerable<Mark> entity)
        {
            context.Marks.RemoveRange(entity);
            SaveChanges();
        }

        public void UpdateMark(Mark entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public Mark FindMarkById(int id)
        {
            return context.Marks.Find(id);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}