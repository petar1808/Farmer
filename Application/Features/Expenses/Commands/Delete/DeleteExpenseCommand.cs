using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Expenses.Commands.Delete
{
    public class DeleteExpenseCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteExpenseCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }
            public async Task<Result> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
            {
                var expense = await farmerDbContext.Expenses
                    .Include(x => x.ExpenseByArableLands)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (expense == null)
                {
                    return $"Разход с Ид: {request.Id} не съществува!";
                }

                farmerDbContext.Expenses.Remove(expense);

                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
