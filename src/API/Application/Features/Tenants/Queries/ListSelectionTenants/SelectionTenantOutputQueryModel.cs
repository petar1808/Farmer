using Application.Models;

namespace Application.Features.Tenants.Queries.ListSelectionTenants
{
    public class SelectionTenantOutputQueryModel : SelectionListModel
    {
        public SelectionTenantOutputQueryModel(
            int value,
            string name) : base(value, name)
        {
        }
    }
}
