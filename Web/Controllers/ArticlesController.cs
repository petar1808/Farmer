using Domain.Enum;
using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Articles;

namespace Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly FarmerDbContext dbContext;

        public ArticlesController(FarmerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddArticleModel articleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(articleModel);
            }

            var articleDate = new Article(articleModel.Name, articleModel.ArticleType);

            dbContext.Add(articleDate);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult All()
        {
            var articles = dbContext
                .Articles
                .Select(x => new ArticleListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ArticleType = x.ArticleType,
                })
                .ToList();

            return View(articles);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = dbContext
                .Articles
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (article == null)
            {
                throw new Exception();
            }

            var editView = new EditArticleViewModel()
            {
                Id = article.Id,
                Name = article.Name,
                ArticleType = article.ArticleType
            };

            return View(editView);
        }

        public bool Edit(
            int id,
            string name,
            ArticleType articleType)
        {
            var articleData = this.dbContext.Articles.FirstOrDefault(x => x.Id == id);

            if (articleData == null)
            {
                return false;
            }

            articleData.UpdateName(name);
            articleData.UpdateArticleType(articleType);

            this.dbContext.Update(articleData);
            this.dbContext.SaveChanges();

            return true;
        }

        [HttpPost]
        public IActionResult Edit(EditArticleViewModel editArticle, int id)
        {
            if (editArticle == null)
            {
                return BadRequest();
            }

            var articleData = this.dbContext.Articles.FirstOrDefault(x => x.Id == id);

            if (editArticle == null)
            {
                return NotFound();
            }

            var articleEdit = Edit(id, editArticle.Name, editArticle.ArticleType);

            if (articleEdit == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artclesData = dbContext.Articles.FirstOrDefault(x => x.Id == id);

            if (artclesData == null)
            {
                return NotFound();
            }

            dbContext.Remove(artclesData);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
