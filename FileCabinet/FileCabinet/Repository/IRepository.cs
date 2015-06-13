﻿using FileCabinet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinet.Repository
{
    public interface IRepository : IDisposable
    {
        IEnumerable<UserProfile> GetAllUsers { get; }
        void AddUser(UserProfile entity);
        void DeleteUser(UserProfile entity);
        void UpdateUser(UserProfile entity);
        UserProfile FindUserById(int id);
        void SaveChanges();

        IEnumerable<Article> GetAllArticles { get; }
        void AddArticle(Article entity);
        void DeleteArticle(Article entity);
        void UpdateArticle(Article entity);
        Article FindArticleById(int id);

        IEnumerable<Mark> GetAllMarks { get; }
        void AddMark(Mark entity);
        void DeleteRangeMark(IEnumerable<Mark> entity);
        void UpdateMark(Mark entity);
        Mark FindMarkById(int id);
    }
}
