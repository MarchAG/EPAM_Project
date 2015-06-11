using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int UserProfileId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public ContentFileType ContentType { get; set; }
        public string DateOfPublication { get; set; }
        public virtual UserProfile User { get; set; }
    }

    public enum ContentFileType
    {
        Text,
        Video,
        Audio
    }
}