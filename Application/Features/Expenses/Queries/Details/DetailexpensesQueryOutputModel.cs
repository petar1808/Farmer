using AutoMapper;
using Domain.Enum;
using Domain.Models;

namespace Application.Features.Expenses.Queries.Details
{
    public class DetailExpensesQueryOutputModel : CommonExpensesQueryOutputModel
    {
        public decimal PricePerUnit { get; set; }

        public decimal Quantity { get; set; }

        public int? ArticleId { get; set; }

        public ExpenseType Type { get; set; }

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Expense, DetailExpensesQueryOutputModel>()
                .ForMember(x => x.SelectedArableLands, cfg => cfg.MapFrom(c => c.ExpenseByArableLands.Select(x => x.ArableLandId)));
        }
    }
}
