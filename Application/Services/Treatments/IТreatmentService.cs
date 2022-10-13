using Application.Models;
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
        Task<Result> Edit(EditТreatmentModel treatmentModel);

        Task<Result> Delete(int id);

        Task<Result> Add(AddТreatmentModel treatmentModel, int seedingId);

        Task<Result<List<ListТreatmentModel>>> List(int seedingId);

        Task<Result<GetTreatmentModel>> Get(int id);
    }
}
