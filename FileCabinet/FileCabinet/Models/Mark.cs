using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Mark
    {
        public int MarkId { get; set; }

        [Index("IX_UserAndArticle", 1, IsUnique = true)]
        public int ArticleId { get; set; }

        [Index("IX_UserAndArticle", 2, IsUnique = true)]
        public int UserProfileId { get; set; }

        public int Value { get; set; }

        public virtual UserProfile User { get; set; }

        public virtual Article Art { get; set; }
    }
}