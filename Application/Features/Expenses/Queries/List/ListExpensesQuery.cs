using Application.Features.Subsidies.Queries.List;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Expenses.Queries.List
{
    public class ListExpensesQuery : IRequest<Result<List<ListExpensesQueryOutputModel>>>
    {
        public int SeasonId { get; set; }

        public class ListExpensesQueryHandler : IRequestHandler<ListExpensesQuery, Result<List<ListExpensesQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ListExpensesQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }
            public async Task<Result<List<ListExpensesQueryOutputModel>>> Handle(
                ListExpensesQuery request, 
                CancellationToken cancellationToken)
            {
                var expenses = await farmerDbContext.Expenses
                    .Include(x => x.ExpenseByArableLands)
                            .ThenInclude(x => x.ArableLand)
                    .Include(x => x.Article)
                    .Where(x => x.WorkingSeasonId == request.SeasonId)
                    .OrderByDescending(x => x.Date)
                    .ToListAsync(cancellationToken);

                var result = mapper.Map<List<ListExpensesQueryOutputModel>>(expenses);

                return result;
            }
        }
    }
}
