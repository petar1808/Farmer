using Application.Extensions;
using Application.Models.Common;
using Application.Services.Articles;
using Application.Services.PerformedWorks;
using Application.Services.Treatments;
using Application.Services.WorikingSeasons;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/assets")]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IWorkingSeasonService workingSeasonService;

        public AssetsController(IArticleService articleService,
            IWorkingSeasonService workingSeasonService)
        {
            this.articleService = articleService;
            this.workingSeasonService = workingSeasonService;
        }

        [HttpGet]
        [Route("articleTypes")]
        public ActionResult<List<SelectionListModel>> GetArticleTypes()
        {
            return EnumHelper.GetAllNamesAndValues<ArticleType>()
                .Select(x => new SelectionListModel(x.Key, x.Value)).ToList();
        }

        [HttpGet]
        [Route("treatment/{type:int}")]
        public async Task<ActionResult<List<SelectionListModel>>> GetArticles(ArticleType type)
        {
            return await articleService
                .ArticlesSelectionList(type)
                .ToActionResult();
        }

        [HttpGet]
        [Route("seasons")]
        public async Task<ActionResult<List<SelectionListModel>>> GetAllSeasons()
        {
            return await workingSeasonService
                .SeasonsSelectionList()
                .ToActionResult();
        }

        [HttpGet]
        [Route("workTypes")]
        public ActionResult<List<SelectionListModel>> GetWorkTypes()
        {
            return EnumHelper.GetAllNamesAndValues<WorkType>()
                .Select(x => new SelectionListModel(x.Key, x.Value)).ToList();
        }

        [HttpGet]
        [Route("treatmentType")]
        public ActionResult<List<SelectionListModel>> GetTreatmentTypes()
        {
            return EnumHelper.GetAllNamesAndValues<ТreatmentType>()
                .Select(x => new SelectionListModel(x.Key, x.Value)).ToList();
        }
    }
}
