using Application.Models;
using Application.Models.Common;
using Application.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Tenants
{
    public interface ITenantService
    {
        Task<Result> Add(AddTenantModel tenant);

        Task<Result<List<ListTenantsWithUsersModel>>> ListTenantsWithUsers();

        Task<Result<List<SelectionListModel>>> ListSelectionTenants();
    }
}
