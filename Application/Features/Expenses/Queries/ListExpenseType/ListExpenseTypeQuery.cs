using Application.Extensions;
using Application.Models;
using Domain.Enum;
using MediatR;

namespace Application.Features.Expenses.Queries.ListExpenseType
{
    public class ListExpenseTypeQuery : IRequest<Result<List<ListExpenseTypeQueryOutputModel>>>
    {
        public class ListExpenseTypeQueryHandler : IRequestHandler<ListExpenseTypeQuery, Result<List<ListExpenseTypeQueryOutputModel>>>
        {
            public async Task<Result<List<ListExpenseTypeQueryOutputModel>>> Handle(
                ListExpenseTypeQuery request,
                CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    return EnumHelper
                            .GetAllNamesAndValues<ExpenseType>()
                            .Select(x => new ListExpenseTypeQueryOutputModel(x.Key, x.Value))
                            .ToList();
                });

                return result;
            }
        }
    }
}
