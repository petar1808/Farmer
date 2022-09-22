using Application.Models.Тreatments;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Treatments
{
    public interface IТreatmentService
    {
        Task Edit(EditТreatmentModel editModel);

        Task Delete(int id);

        Task Add(AddТreatmentModel treatmentModel, int seedingId);

        Task<List<ListТreatmentModel>> List(int seedingId);

        Task<GetTreatmentModel> Get(int id);
    }
}
