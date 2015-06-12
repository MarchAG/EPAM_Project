using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileCabinet.Models
{
    public class Mark
    {
        public int MarkId { get; set; }

        public int ArticleId { get; set; }

        public int UserProfileId { get; set; }

        public int Value { get; set; }

        public virtual UserProfile User { get; set; }

        public virtual Article Art { get; set; }
    }
}