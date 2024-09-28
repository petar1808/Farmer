using Application.Mappings;
using Domain.Models;

namespace Application.Features.Subsidies.Queries
{
    public class CommonSubsidyOutputQueryModel : IMapFrom<Subsidy>
    {
        public int Id { get; set; }

        public decimal Income { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
