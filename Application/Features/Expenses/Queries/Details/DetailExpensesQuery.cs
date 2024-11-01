using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Expenses.Queries.Details
{
    public class DetailExpensesQuery : IRequest<Result<DetailExpensesQueryOutputModel>>
    {
        public int ExpenseId { get; set; }

        public class DetailExpensesQueryHandler : IRequestHandler<DetailExpensesQuery, Result<DetailExpensesQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public DetailExpensesQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }
            public async Task<Result<DetailExpensesQueryOutputModel>> Handle(
                DetailExpensesQuery request,
                CancellationToken cancellationToken)
            {
                var expense = farmerDbContext.Expenses.AsQueryable();

                var result = await mapper
                    .ProjectTo<DetailExpensesQueryOutputModel>(expense)
                    .FirstOrDefaultAsync(x => x.Id == request.ExpenseId);

                if (result == null)
                {
                    return $"Разход с Ид: {request.ExpenseId} не съществува";
                }

                return result;
            }
        }
    }
}
