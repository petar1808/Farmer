using Application.Extensions;
using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Articles
{
    public class ListArticleModel : IMapFrom<Article>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public string ArticleType { get; init; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Article, ListArticleModel>()
                .ForMember(x => x.ArticleType, cfg => cfg.MapFrom(c => c.ArticleType.GetEnumDisplayName()));
    }
}
