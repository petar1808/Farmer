using Application.Extensions;
using Application.Models;
using Domain.Enum;
using MediatR;

namespace Application.Features.PerformedWorks.Queries.ListWorkType
{
    public class WorkTypeListQuery : IRequest<Result<List<WorkTypeOutputQueryModel>>>
    {
        public class WorkTypeListQueryHandler : IRequestHandler<WorkTypeListQuery, Result<List<WorkTypeOutputQueryModel>>>
        {
            public async Task<Result<List<WorkTypeOutputQueryModel>>> Handle(
                WorkTypeListQuery request,
                CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    return EnumHelper
                            .GetAllNamesAndValues<WorkType>()
                            .Select(x => new WorkTypeOutputQueryModel(x.Key, x.Value))
                            .OrderBy(x => x.Name)
                            .ToList();
                });

                return result;
            }
        }
    }
}
