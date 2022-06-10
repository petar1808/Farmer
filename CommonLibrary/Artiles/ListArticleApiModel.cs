using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Artiles
{
    public class ListArticleApiModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string ArticleType { get; set; } = default!;
    }
}
