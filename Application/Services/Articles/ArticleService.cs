using Application.Models;
using Application.Models.Articles;
using Application.Models.Common;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public ArticleService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Add(AddArticleModel articleModel)
        {
            var article = new Article(articleModel.Name, articleModel.ArticleType);

            await farmerDbContext.AddAsync(article);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Delete(int id)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (article == null)
            {
                return $"Артикул с Ид: {id} не съществува!";
            }

            var seedingArticle = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.ArticleId == id);

            if (seedingArticle)
            {
                return $"Артикул с Ид: {id} нe може да бъде изтрит, защото е създадена сеитба с този артикул";
            }

            var treatmentArticle = await farmerDbContext
                .Treatments
                .AnyAsync(x => x.ArticleId == id);

            if (treatmentArticle)
            {
                return $"Артикул с Ид: {id} нe може да бъде изтрит, защото е създанено третиране с този артикул!";
            }

            farmerDbContext.Articles.Remove(article);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditArticleModel articleModel)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == articleModel.Id);

            if (article == null)
            {
                return $"Артикул с Ид: {articleModel.Id} не съществува!";
            }

            article
                .UpdateName(articleModel.Name)
                .UpdateArticleType(articleModel.ArticleType);

            farmerDbContext.Update(article);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<GetArticleModel>> Get(int id)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (article == null)
            {
                return $"Артикул с Ид: {id} не съществува!";
            }

            var result = mapper.Map<GetArticleModel>(article);
            return result;
        }

        public async Task<Result<List<ListArticleModel>>> List()
        {
            var articles = await farmerDbContext.Articles.ToListAsync();

            var result = mapper.Map<List<ListArticleModel>>(articles);
            return result;
        }

        public async Task<Result<List<SelectionListModel>>> SeedsArticlesSelectionList()
        {
            var articles = await farmerDbContext.Articles
                .Where(x => x.ArticleType == ArticleType.Seeds)
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return articles;
        }

        public async Task<Result<List<SelectionListModel>>> TreatmentArticlesSelectionList()
        {
            var articles = await farmerDbContext.Articles
                .Where(x => x.ArticleType != ArticleType.Seeds)
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return articles;
        }
    }     
}
