using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Articles
{
    public class ArticleBaseModel
    {
        public string Name { get; init; } = default!;

        public ArticleType ArticleType { get; init; }
    }
}
