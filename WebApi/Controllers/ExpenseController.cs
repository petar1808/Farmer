using Application.Features.Expenses.Commands.Create;
using Application.Features.Expenses.Commands.Delete;
using Application.Features.Expenses.Commands.Edit;
using Application.Features.Expenses.Queries.Details;
using Application.Features.Expenses.Queries.List;
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
            [FromRoute] ListExpensesQuery query)
            => await this.Send(query);

        [HttpGet]
        [Route("{expenseId:int}")]
        public async Task<ActionResult<DetailExpensesQueryOutputModel>> Details(
            [FromRoute] DetailExpensesQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateExpenseCommand command)
            => await this.Send(command);

        [HttpPut]
        public async Task<ActionResult> Edit(
            [FromBody] EditExpenseCommand command)
            => await this.Send(command);

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteExpenseCommand command)
            => await this.Send(command);

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
