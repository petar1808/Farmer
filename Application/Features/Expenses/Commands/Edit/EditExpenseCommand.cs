using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Expenses.Commands.Edit
{
    public class EditExpenseCommand : EditExpenseInputModel, IRequest<Result>
    {
        public class EditExpenseCommandHandler : IRequestHandler<EditExpenseCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditExpenseCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(EditExpenseCommand request, CancellationToken cancellationToken)
            {
                var expense = await farmerDbContext.Expenses
                    .Include(x => x.ExpenseByArableLands)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (expense == null)
                {
                    return $"Разгод с Ид:{request.Id} не съществува!";
                }

                expense
                    .UpdateDate(request.Date)
                    .UpdateType(request.Type)
                    .UpdatePricePerUnit(request.PricePerUnit)
                    .UpdateQuantity(request.Quantity)
                    .UpdateArticleId(request.ArticleId);

                if (request.SelectedArableLands.Any())
                {
                    var newExpenseByArableLands = await CalculateExpenseByArableLand(
                        request.SelectedArableLands,
                        expense.Sum,
                        cancellationToken);

                    expense.UpdateExpenseByArableLands(newExpenseByArableLands);
                }

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
                    new ExpenseByArableLand(
                        arableLand.Id,
                        ((decimal)arableLand.SizeInDecar / (decimal)totalArea) * sum))
                    .ToList();
            }
        }
    }
}
