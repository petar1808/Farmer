using Application.Extensions;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Articles.Queries.List
{
    public class ArticleListQueryOutputModel : CommonArticleOutputQueryModel, IMapFrom<Article>
    {
        public string ArticleType { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Article, ArticleListQueryOutputModel>()
                .ForMember(x => x.ArticleType, cfg => cfg.MapFrom(c => c.ArticleType.GetEnumDisplayName()));
    }
}
