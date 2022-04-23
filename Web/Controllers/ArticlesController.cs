using Application.Models.Articles;
using Application.Services.Articles;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Articles;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesController(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(AddArticleViewModel articleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(articleModel);
            }

            await articleService.Add(articleModel.Name, articleModel.ArticleType);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var articles = await articleService.GetAll();

            return View(mapper.Map<List<GetArticleViewModel>>(articles));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await articleService.Get(id);

            return View(mapper.Map<EditArticleViewModel>(result));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            if (article == null)
            {
                return BadRequest();
            }

            await articleService.Edit(
                article.Id,
                article.Name,
                article.ArticleType);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await articleService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
