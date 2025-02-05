using Application.Models;
using Application.Services;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reporting.Queries.FinancialSummary
{
    public class FinancialSummaryReportQuery : IRequest<Result<List<FinancialSummaryReportOutputModel>>>
    {
        public class FinancialSummaryReportQueryHandler : IRequestHandler<FinancialSummaryReportQuery, Result<List<FinancialSummaryReportOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public FinancialSummaryReportQueryHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<FinancialSummaryReportOutputModel>>> Handle(
                FinancialSummaryReportQuery request,
                CancellationToken cancellationToken)
            {
                var result = new List<FinancialSummaryReportOutputModel>();

                var seedingTask = await farmerDbContext.Seedings
                    .Select(x => new
                    {
                        x.ArableLandId,
                        x.WorkingSeasonId,
                        WorkingSeasonName = x.WorkingSeason.Name,
                        ArableLandName = x.ArableLand.Name,
                        Income = x.ArableLand.SizeInDecar * (x.HarvestedGrainSellingPricePerKilogram * x.HarvestedQuantityPerDecare)
                    })
                    .ToListAsync(cancellationToken);

                var subsidiesTask = await farmerDbContext.Subsidies
                    .Include(x => x.SubsidyByArableLands)
                    .ToListAsync(cancellationToken);

                var expensesTask = await farmerDbContext.Expenses
                    .Include(x => x.ExpenseByArableLands)
                        .ThenInclude(x => x.ArableLand)
                    .ToListAsync(cancellationToken);

                var workingSeasonsTask = await farmerDbContext.WorkingSeasons
                    .OrderByDescending(c => c.StartDate)
                    .ToListAsync(cancellationToken);

                var seeding = seedingTask;
                var subsidies = subsidiesTask;
                var expenses = expensesTask;
                var workingSeasons = workingSeasonsTask;

                foreach (var workingSeason in workingSeasons)
                {
                    var seasonId = workingSeason.Id;
                    var incomeHarvestingForSeason = seeding
                        .Where(x => x.WorkingSeasonId == seasonId)
                        .Sum(x => x.Income);

                    var subsidyForSeason = subsidies
                        .Where(x => x.WorkingSeasonId == seasonId)
                        .Sum(x => x.SubsidyByArableLands.Select(x => x.Income).Sum());

                    var expenseForSeason = expenses
                        .Where(x => x.WorkingSeasonId == seasonId)
                        .GroupBy(x => x.Type)
                        .ToDictionary(g => g.Key, g => g.Sum(e => e.Sum));

                    var resultByWorkingSeason = new FinancialSummaryReportOutputModel
                    {
                        Id = seasonId,
                        WorkingSeason = workingSeason.Name,
                        SumIncome = incomeHarvestingForSeason + subsidyForSeason,
                        SumExpense = expenseForSeason
                            .Where(x => x.Key != ExpenseType.Machinery)
                            .Sum(x => x.Value),
                        ExpensesForMachinery = expenseForSeason
                            .GetValueOrDefault(ExpenseType.Machinery, 0)
                    };

                    // Group seeding, subsidy, and expense data by arable land for each working season
                    var incomesByArableLand = seeding
                        .Where(x => x.WorkingSeasonId == seasonId)
                        .GroupBy(x => x.ArableLandId)
                        .Select(g => new FinancialSummaryReportIncomes
                        {
                            ArableLandName = g.First().ArableLandName,
                            Harvest = g.Sum(x => x.Income),
                            Subsidies = subsidies
                                .Where(s => s.WorkingSeasonId == seasonId)
                                .SelectMany(s => s.SubsidyByArableLands)
                                .Where(sa => sa.ArableLandId == g.Key)
                                .Sum(sa => sa.Income)
                        })
                        .OrderBy(x => x.ArableLandName)
                        .ToList();

                    var expensesByArableLand = expenses
                        .Where(x => x.WorkingSeasonId == seasonId)
                        .SelectMany(x => x.ExpenseByArableLands.Select(e => new
                        {
                            e.ArableLandId,
                            e.Sum,
                            ArableLandName = e.ArableLand.Name,
                            ExpenseType = x.Type
                        }))
                        .GroupBy(e => e.ArableLandId)
                        .Select(g => new FinancialSummaryReportExpenses
                        {
                            ArableLandName = g.First().ArableLandName,
                            Fuel = g.Where(x => x.ExpenseType == ExpenseType.Fuel).Sum(x => x.Sum),
                            Pesticides = g.Where(x => x.ExpenseType == ExpenseType.Pesticides).Sum(x => x.Sum),
                            Fertilizers = g.Where(x => x.ExpenseType == ExpenseType.Fertilizers).Sum(x => x.Sum),
                            Seeds = g.Where(x => x.ExpenseType == ExpenseType.Seeds).Sum(x => x.Sum),
                            Rent = g.Where(x => x.ExpenseType == ExpenseType.Rent).Sum(x => x.Sum),
                            Harvest = g.Where(x => x.ExpenseType == ExpenseType.Harvest).Sum(x => x.Sum),
                        })
                        .OrderBy(x => x.ArableLandName)
                        .ToList();

                    resultByWorkingSeason.IncomesByArableLand = incomesByArableLand;
                    resultByWorkingSeason.ExpensesByArableLand = expensesByArableLand;

                    result.Add(resultByWorkingSeason);
                }

                return result;
            }
        }
    }
}
