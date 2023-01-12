using Application.Models;
using Application.Models.Common;
using Application.Models.WorkingSeasons;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WorikingSeasons
{
    public class WorkingSeasonService : IWorkingSeasonService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public WorkingSeasonService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Add(AddWorkingSeasonModel workingSeasonModel)
        {
            var workingSeasonDate = await farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.StartDate.Year == workingSeasonModel.StartDate.Year && x.EndDate.Year == workingSeasonModel.EndDate.Year);

            if (workingSeasonDate)
            {
                return "Има съзаден сезон със същото начало и край";
            }

            var workingSeason = new WorkingSeason(
                workingSeasonModel.Name,
                workingSeasonModel.StartDate,
                workingSeasonModel.EndDate);

            await this.farmerDbContext.AddAsync(workingSeason);
            await this.farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Delete(int id)
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workingSeason == null)
            {
                return $"Сезон с Ид: {id} не съществува!";
            }

            var workingSeasonSeeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.WorkingSeasonId == id);

            if (workingSeasonSeeding)
            {
                return $"Сезон с Ид: {id} не може да бъде изтрит, защото е създадена сеитба за този сезон!";
            }

            farmerDbContext.WorkingSeasons.Remove(workingSeason);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditWorkingSeasonModel workingSeasonModel)
        {
            var workingSeason = farmerDbContext
                .WorkingSeasons
                .FirstOrDefault(x => x.Id == workingSeasonModel.Id);

            if (workingSeason == null)
            {
                return $"Сезон с Ид: {workingSeasonModel.Id} не съществува!";
            }

            workingSeason
                .UpdateName(workingSeasonModel.Name)
                .UpdateSratDate(workingSeasonModel.StartDate)
                .UpdateEndDate(workingSeasonModel.EndDate);

            this.farmerDbContext.Update(workingSeason);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<GetWorkingSeasonModel>> Get(int id)
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workingSeason == null)
            {
                return $"Сезон с Ид: {id} не съществува!";
            }

            var result = mapper.Map<GetWorkingSeasonModel>(workingSeason);
            return result;
        }

        public async Task<Result<List<ListWorkingSeasonBalanceModel>>> ListWorkingSeasonsBalance()
        {
            var workingSeasonBalance = await farmerDbContext
                .WorkingSeasons
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.ArableLand)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.Article)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.PerformedWorks)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.Subsidies)
                .Include(x => x.Seedings)
                    .ThenInclude(x => x.Treatments)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();

            var result = mapper.Map<List<ListWorkingSeasonBalanceModel>>(workingSeasonBalance);

            return result;
        }


        public async Task<Result<List<SelectionListModel>>> SeasonsSelectionList()
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return workingSeason;
        }
    }
}
