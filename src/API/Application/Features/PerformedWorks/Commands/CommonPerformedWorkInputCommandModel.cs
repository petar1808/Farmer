using Domain.Enum;

namespace Application.Features.PerformedWorks.Commands
{
    public class CommonPerformedWorkInputCommandModel
    {
        public WorkType WorkType { get; set; }

        public DateTime Date { get; set; }
    }
}
