using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Login
    {
        public int LoginId { get; set; }
        [Required(ErrorMessage = "Введите Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Article> PersonalArticles { get; set; }
    }
}