namespace Application.Features.Tenants.Queries.ListSelectionTenants
{
    public class SelectionTenantOutputQueryModel
    {
        public SelectionTenantOutputQueryModel(
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
