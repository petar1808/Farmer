using Application.Models;

namespace Application.Features.Treatments.Queries.ListTreatmentType
{
    public class TreatmentTypeOutputQueryModel : SelectionListModel
    {
        public TreatmentTypeOutputQueryModel(
           int value,
           string name) : base(value, name)
        {

        }
    }
}
