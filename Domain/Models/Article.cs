using Domain.Common;
using Domain.Enum;
using static Domain.ModelConstraint.CommonConstraints;

namespace Domain.Models
{
    public class Article : Entity<int>, ITenant
    {
        public Article(string name, ArticleType articleType)
        {
            Validate(name,articleType); 
            Name = name;
            ArticleType = articleType;
        }

        public string Name { get; private set; }

        public ArticleType ArticleType { get; private set; }

        public int TenantId { get; set; }

        public Article UpdateName(string name)
        {
            ValidateName(name);
            this.Name = name;
            return this;
        }

        public Article UpdateArticleType(ArticleType articleType)
        {
            ValidateArticleType(articleType);
            this.ArticleType = articleType;
            return this;
        }

        private void ValidateName(string name)
          => Guard.Guard.ForStringMaxLength(
              name,
              MaxNameLenght,
              nameof(this.Name));

        private void ValidateArticleType(ArticleType type)
            => Guard.Guard.ForValidEnum<ArticleType>((int)type, nameof(ArticleType));

        private void Validate(string name, ArticleType type)
        {
            ValidateName(name);
            ValidateArticleType(type);
        }
    }
}
