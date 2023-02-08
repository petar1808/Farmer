using Application.Extensions;
using Application.Models;
using Domain.Enum;
using MediatR;

namespace Application.Features.Treatments.Queries.ListTreatmentType
{
    public class TreatmentTypeListQuery : IRequest<Result<List<TreatmentTypeOutputQueryModel>>>
    {
        public class TreatmentTypeListQueryHandler : IRequestHandler<TreatmentTypeListQuery, Result<List<TreatmentTypeOutputQueryModel>>>
        {
            public async Task<Result<List<TreatmentTypeOutputQueryModel>>> Handle(
                TreatmentTypeListQuery request,
                CancellationToken cancellationToken)
            {
                var result = await Task.Run(() =>
                {
                    return EnumHelper
                            .GetAllNamesAndValues<ТreatmentType>()
                            .Select(x => new TreatmentTypeOutputQueryModel(x.Key, x.Value))
                            .ToList();
                });

                return result;
            }
        }
    }
}
