using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Register
    {
        [Display(Name = "Имя пользователя")]
        [Required(ErrorMessage = "Поле должно быть заполненым")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполненым")]
        [Display(Name = "Пароль")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пороль должен быть от 5 до 20 символов")]
        [Compare("ConfPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтведрить пароль")]
        public string ConfPassword { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполненым")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string EMail { get; set; }
    }
}