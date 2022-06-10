using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Artiles
{
    public class ArticleBaseApiModel
    {
        public string Name { get; set; } = default!;

        public int ArticleType { get; set; }
    }
}
