using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Tenants
{
    public class CreateAdminModel
    {
        public string UserEmail { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string ActivateUserUrl { get; set; } = default!;

        public int TenantId { get; set; }
    }
}
