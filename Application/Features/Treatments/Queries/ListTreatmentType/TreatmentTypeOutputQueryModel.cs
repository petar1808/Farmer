namespace Application.Features.Treatments.Queries.ListTreatmentType
{
    public class TreatmentTypeOutputQueryModel
    {
        public TreatmentTypeOutputQueryModel(
           int value,
           string name)
        {
            this.Value = value;
            this.Name = name;
        }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
