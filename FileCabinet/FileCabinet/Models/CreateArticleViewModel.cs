using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class CreateArticleViewModel
    {
        [Display(Name = "Заголовок статьи")]
        [Required(ErrorMessage = "Поле должно быть заполненым")]
        public string Title { get; set; }

        [Display(Name = "Загрузите файл")]
        [Required(ErrorMessage = "Поле должно быть заполненым")]
        public HttpPostedFileBase ContentFile { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполненым")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Tags { get; set; }

        public int GetFileType()
        {
            if (ContentFile == null)
                throw new ArgumentNullException();
            string extension = Path.GetExtension(ContentFile.FileName);
            extension.ToLower();
            switch (extension)
            {
                case ".mp4":
                case ".webm":
                    return 1;
                case ".mp3":
                case ".waw":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}