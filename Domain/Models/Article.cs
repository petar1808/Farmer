using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Article : Entity<int>
    {
        public Article(string name, ArticleType articleType)
        {
            Name = name;
            ArticleType = articleType;
        }

        public string Name { get; private set; }

        public ArticleType ArticleType { get; private set; }

        public Article UpdateName(string name)
        {
            this.Name = name;
            return this;
        }

        public Article UpdateArticleType(ArticleType articleType)
        {
            this.ArticleType = articleType;
            return this;
        }
    }
}
