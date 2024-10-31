using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Expenses.Commands.Create
{
    public class CreateExpenseCommand : CreateExpenseInputModel, IRequest<Result>
    {
        public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateExpenseCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateExpenseCommand request, 
                CancellationToken cancellationToken)
            {
                var expense = new Expense(
                    request.Date,
                    request.Type,
                    request.PricePerUnit,
                    request.Quantity,
                    request.ArticleId,
                    request.WorkingSeasonId);

                if (request.SelectedArableLands.Any())
                {
                    expense.AddExpenseByArableLands(
                        await CalculateExpenseByArableLand(request.SelectedArableLands, expense.Sum, cancellationToken)
                        );
                }

                await farmerDbContext.AddAsync(expense, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }

            private async Task<List<ExpenseByArableLand>> CalculateExpenseByArableLand(
                IEnumerable<int> selectedArableLands,
                decimal sum,
                CancellationToken cancellationToken)
            {
                var arableLands = await farmerDbContext.ArableLands
                    .Where(x => selectedArableLands.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                var totalArea = arableLands.Sum(x => x.SizeInDecar);

                return arableLands.Select(arableLand =>
                    new ExpenseByArableLand(arableLand.Id, ((decimal)arableLand.SizeInDecar / (decimal)totalArea) * sum))
                    .ToList();
            }
        }
    }
}
