using Application.Features.Expenses.Commands.Create;
using Application.Features.Expenses.Queries.Details;
using Application.Features.Expenses.Queries.List;
using Application.Features.Subsidies.Commands.Create;
using Domain;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/expense")]
    [Authorize(Roles = $"{IdentityRoles.AdminRole},  {IdentityRoles.UserRole}")]
    public class ExpenseController : BaseApiController
    {
        [HttpGet]
        [Route("list/{seasonId:int}")]
        public async Task<ActionResult<List<ListExpensesQueryOutputModel>>> ListExpenses(
            [FromRoute] ListExpensesQuery subsidyListQuery)
            => await this.Send(subsidyListQuery);

        [HttpGet]
        [Route("{expenseId:int}")]
        public async Task<ActionResult<DetailExpensesQueryOutputModel>> Details(
            [FromRoute] DetailExpensesQuery subsidyListQuery)
            => await this.Send(subsidyListQuery);

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateExpenseCommand subsidyModel)
            => await this.Send(subsidyModel);

        [HttpGet]
        [Route("configurations")]
        public async Task<ActionResult<List<ExpensesConfigurations>>> GetExpensesConfigurations()
        {
            var expenses = new List<ExpensesConfigurations>()
            {
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Fuel,
                    DistributeByArableLand = true,
                    ArticleType = null,
                    ShowPricePerUnit = true
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Machinery,
                    DistributeByArableLand = false,
                    ArticleType = null,
                    ShowPricePerUnit = false
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Pesticides,
                    DistributeByArableLand = true,
                    ArticleType = ArticleType.Preparations,
                    ShowPricePerUnit = true
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Fertilizers,
                    DistributeByArableLand = true,
                    ArticleType = ArticleType.Fertilizers,
                    ShowPricePerUnit = true
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Seeds,
                    DistributeByArableLand = true,
                    ArticleType = ArticleType.Seeds,
                    ShowPricePerUnit = true
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Rent,
                    DistributeByArableLand = true,
                    ArticleType = null,
                    ShowPricePerUnit = false
                },
                new ExpensesConfigurations
                {
                    ExpenseType = ExpenseType.Harvest,
                    DistributeByArableLand = true,
                    ArticleType = null,
                    ShowPricePerUnit = false
                }
            };

            var result = await Task.Run(() =>
            {
                return expenses;
            });

            return result;
        }
            
    }
}
