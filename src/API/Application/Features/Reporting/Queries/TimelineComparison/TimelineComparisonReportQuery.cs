using Application.Extensions;
using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reporting.Queries.TimelineComparison
{
    public class TimelineComparisonReportQuery : IRequest<Result<TimelineComparisonReportOutputModel>>
    {
        public int SeedingId1 { get; set; }
        public int SeedingId2 { get; set; }

        public class TimelineComparisonReportQueryHandler : IRequestHandler<TimelineComparisonReportQuery, Result<TimelineComparisonReportOutputModel>>
        {
            private readonly IFarmerDbContext _farmerDbContext;

            public TimelineComparisonReportQueryHandler(IFarmerDbContext farmerDbContext)
            {
                _farmerDbContext = farmerDbContext;
            }

            public async Task<Result<TimelineComparisonReportOutputModel>> Handle(
                TimelineComparisonReportQuery request,
                CancellationToken cancellationToken)
            {
                var arableLand1 = await GetArableLandTimelineAsync(request.SeedingId1);
                var arableLand2 = await GetArableLandTimelineAsync(request.SeedingId2);

                var seeding1 = await GetSeedingDetailsAsync(request.SeedingId1);
                var seeding2 = await GetSeedingDetailsAsync(request.SeedingId2);

                if (seeding1 == null || seeding2 == null)
                {
                    return "Не е намерена сеидба";
                }

                var result = new TimelineComparisonReportOutputModel
                {
                    SeedName1 = seeding1.Article?.Name ?? string.Empty,
                    SeedsQuantityPerDecare1 = seeding1.SeedsQuantityPerDecare,
                    HarvestedQuantityPerDecare1 = seeding1.HarvestedQuantityPerDecare,
                    ArableLand1 = arableLand1.OrderBy(x => x.DateTime).ToList(),
                    SeedName2 = seeding2.Article?.Name ?? string.Empty,
                    SeedsQuantityPerDecare2 = seeding2.SeedsQuantityPerDecare,
                    HarvestedQuantityPerDecare2 = seeding2.HarvestedQuantityPerDecare,
                    ArableLand2 = arableLand2.OrderBy(x => x.DateTime).ToList()
                };

                return result;
            }

            private async Task<List<ArableLandTimeLine>> GetArableLandTimelineAsync(int seedingId)
            {
                var treatmentTimeline = await _farmerDbContext.Treatments
                    .AsNoTracking()
                    .Where(x => x.Seeding.Id == seedingId)
                    .Select(x => new ArableLandTimeLine
                    {
                        DateTime = x.Date,
                        Icon = "science",
                        Value = $"{x.TreatmentType.GetEnumDisplayName()} с {x.Article.Name}",
                        AdditionalValue = $"Кол. на декар: {x.ArticleQuantity.ToString("0.####")} кг/л"
                    })
                    .ToListAsync();

                var performedWorksTimeline = await _farmerDbContext.PerformedWorks
                    .AsNoTracking()
                    .Where(x => x.Seeding.Id == seedingId)
                    .Select(x => new ArableLandTimeLine
                    {
                        DateTime = x.Date,
                        Icon = "agriculture",
                        Value = x.WorkType.GetEnumDisplayName()
                    })
                    .ToListAsync();

                return treatmentTimeline.Concat(performedWorksTimeline).ToList();
            }

            private async Task<Seeding?> GetSeedingDetailsAsync(int seedingId)
            {
                return await _farmerDbContext.Seedings
                    .Include(x => x.Article)
                    .FirstOrDefaultAsync(x => x.Id == seedingId);
            }
        }
    }
}
