using Application.Extensions;
using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Features.Reporting.Queries.TimelineComparison
{
    public class TimelineComparisonReportQuery : IRequest<Result<TimelineComparisonReportOutputModel>>
    {
        public int ArableLandId1 { get; set; }

        public int WorkingSeasonId1 { get; set; }

        public int WorkingSeasonId2 { get; set; }

        public int ArableLandId2 { get; set; } 


        public class TimelineComparisonReportQueryHandler : IRequestHandler<TimelineComparisonReportQuery, Result<TimelineComparisonReportOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public TimelineComparisonReportQueryHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<TimelineComparisonReportOutputModel>> Handle(
                TimelineComparisonReportQuery request, 
                CancellationToken cancellationToken)
            {
                request.WorkingSeasonId1 = 1;
                request.WorkingSeasonId2 = 2;
                request.ArableLandId1 = 1;
                request.ArableLandId2 = 2;

                var arableLand1 = new List<ArableLandTimeLine>();
                var arableLand2 = new List<ArableLandTimeLine>();

                //arableLand1.AddRange(await farmerDbContext
                //    .ExpenseByArableLands
                //    .Where(x => x.Expense.WorkingSeasonId == request.WorkingSeasonId1
                //                && x.ArableLandId == request.ArableLandId1)
                //    .Select(x => new ArableLandTimeLine
                //    {
                //        DateTime = x.Expense.Date,
                //        Icon = "receipt",
                //        Type = $"Разход({x.Expense.Type.GetEnumDisplayName()})",
                //        Value = x.Sum.ToString("C", new CultureInfo("bg-BG")).Replace(" BGN", " лв"),
                //        AdditionalValue = x.Expense.Article == null ? null : x.Expense.Article.Name
                //    })
                //    .ToListAsync(cancellationToken)
                //    );

                //arableLand1.AddRange(await farmerDbContext
                //    .SubsidyByArableLands
                //    .Where(x => x.Subsidy.WorkingSeasonId == request.WorkingSeasonId1
                //            && x.ArableLandId == request.ArableLandId1)
                //    .Select(x => new ArableLandTimeLine
                //    {
                //        DateTime = x.Subsidy.Date,
                //        Icon = "monetization_on",
                //        Type = "Субсидия",
                //        Value = x.Income.ToString("C", new CultureInfo("bg-BG")).Replace(" BGN", " лв"),
                //        AdditionalValue = x.Subsidy.Comment
                //    })
                //    .ToListAsync()
                //    );

                arableLand1.AddRange(await farmerDbContext
                    .Treatments
                    .Where(x => x.Seeding.WorkingSeasonId == request.WorkingSeasonId1
                        && x.Seeding.ArableLandId == request.ArableLandId1)
                    .Select(x => new ArableLandTimeLine
                    {
                        DateTime = x.Date,
                        Icon = "science",
                        Type = $"Третиране{x.TreatmentType.GetEnumDisplayName()}",
                        Value = x.Article.Name,
                        AdditionalValue = $"Количество {x.ArticleQuantity} кг/л"
                    })
                    .ToListAsync());

                arableLand1.AddRange(await farmerDbContext
                    .PerformedWorks
                    .Where(x => x.Seeding.WorkingSeasonId == request.WorkingSeasonId1
                        && x.Seeding.ArableLandId == request.ArableLandId1)
                    .Select(x => new ArableLandTimeLine
                    {
                        DateTime = x.Date,
                        Icon = "agriculture",
                        Type = "Обработки",
                        Value = x.WorkType.GetEnumDisplayName()
                    })
                    .ToListAsync());

                //arableLand2.AddRange(await farmerDbContext
                //    .ExpenseByArableLands
                //    .Where(x => x.Expense.WorkingSeasonId == request.WorkingSeasonId2
                //                && x.ArableLandId == request.ArableLandId2)
                //    .Select(x => new ArableLandTimeLine
                //    {
                //        DateTime = x.Expense.Date,
                //        Icon = "receipt",
                //        Type = $"Разход({x.Expense.Type.GetEnumDisplayName()})",
                //        Value = x.Sum.ToString("C", new CultureInfo("bg-BG")).Replace(" BGN", " лв"),
                //        AdditionalValue = x.Expense.Article == null ? null : x.Expense.Article.Name
                //    })
                //    .ToListAsync(cancellationToken)
                //    );

                //arableLand2.AddRange(await farmerDbContext
                //    .SubsidyByArableLands
                //    .Where(x => x.Subsidy.WorkingSeasonId == request.WorkingSeasonId2
                //            && x.ArableLandId == request.ArableLandId2)
                //    .Select(x => new ArableLandTimeLine
                //    {
                //        DateTime = x.Subsidy.Date,
                //        Icon = "monetization_on",
                //        Type = "Субсидия",
                //        Value = x.Income.ToString("C", new CultureInfo("bg-BG")).Replace(" BGN", " лв"),
                //        AdditionalValue = x.Subsidy.Comment
                //    })
                //    .ToListAsync()
                //    );

                arableLand2.AddRange(await farmerDbContext
                        .Treatments
                        .Where(x => x.Seeding.WorkingSeasonId == request.WorkingSeasonId2
                            && x.Seeding.ArableLandId == request.ArableLandId2)
                        .Select(x => new ArableLandTimeLine
                        {
                            DateTime = x.Date,
                            Icon = "science",
                            Type = $"Третиране{x.TreatmentType.GetEnumDisplayName()}",
                            Value = x.Article.Name,
                            AdditionalValue = $"Количество {x.ArticleQuantity} кг/л" 
                        })
                        .ToListAsync());

                arableLand2.AddRange(await farmerDbContext
                    .PerformedWorks
                    .Where(x => x.Seeding.WorkingSeasonId == request.WorkingSeasonId2
                        && x.Seeding.ArableLandId == request.ArableLandId2)
                    .Select(x => new ArableLandTimeLine
                    {
                        DateTime = x.Date,
                        Icon = "agriculture",
                        Type = "Обработки",
                        Value = x.WorkType.GetEnumDisplayName()
                    })
                    .ToListAsync());

                var result = new TimelineComparisonReportOutputModel()
                {
                    ArableLand1 = arableLand1.OrderBy(x => x.DateTime).ToList(),
                    ArableLand2 = arableLand2.OrderBy(x => x.DateTime).ToList(),
                };

                return result;
            }
        }
    }
}
