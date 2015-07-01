using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileCabinet.Models
{
    public class EditArticleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Заголовок статьи")]
        [Required(ErrorMessage = "Поле должно быть заполненым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполненым")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Tags { get; set; }
    }
}