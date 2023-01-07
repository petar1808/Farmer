using Application.Mappings;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Subsidies
{
    public class ListSubsidiesModel : SubsidyBaseModel, IMapFrom<Subsidy>
    {
        public int Id { get; init; }
    }
}
