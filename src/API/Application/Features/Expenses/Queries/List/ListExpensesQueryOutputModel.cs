using Application.Extensions;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Expenses.Queries.List
{
    public class ListExpensesQueryOutputModel : CommonExpensesQueryOutputModel
    {
        public string Type { get; set; } = string.Empty;

        public string Article { get; set; } = string.Empty;

        public Dictionary<string, decimal> ExpensesByArableLand { get; set; } = new Dictionary<string, decimal>();

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Expense, ListExpensesQueryOutputModel>()
                    .ForMember(x => x.Type, cfg => cfg.MapFrom(c => c.Type.GetEnumDisplayName()))
                    .ForMember(x => x.Article, cfg => cfg.Condition(c => c.Article is not null))
                    .ForMember(x => x.Article, cfg => cfg.MapFrom(c => c.Article!.Name))
                    .ForMember(x => x.ExpensesByArableLand,
                           cfg => cfg.MapFrom(c => c.ExpenseByArableLands.ToDictionary(k => k.ArableLand.Name, v => v.Sum)));
        }
    }
}
