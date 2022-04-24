using Application.Services.WorikingSeasons;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmerWeb.Test.Mocks
{
    public class WorkingSeasonServiceMock
    {
        public static IWorkingSeasonService InstanceEdit
        {
            get
            {
                var articleServiceMock = new Mock<IWorkingSeasonService>();

                articleServiceMock
                    .Setup(a => a.Edit(1, "2021/2022", DateTime.Today, DateTime.Today.AddYears(1)));


                return articleServiceMock.Object;
            }
        }

        public static IWorkingSeasonService InstanceDelete
        {
            get
            {
                var articleServiceMock = new Mock<IWorkingSeasonService>();

                articleServiceMock
                    .Setup(a => a.Delete(1));


                return articleServiceMock.Object;
            }
        }

        public static IWorkingSeasonService InstanceGet
        {
            get
            {
                var articleServiceMock = new Mock<IWorkingSeasonService>();

                articleServiceMock
                    .Setup(a => a.Get(1));


                return articleServiceMock.Object;
            }
        }


        public static IWorkingSeasonService InstanceAdd
        {
            get
            {
                var articleServiceMock = new Mock<IWorkingSeasonService>();

                articleServiceMock
                    .Setup(a => a.Add("2021/2022", DateTime.Today, DateTime.Today.AddYears(1)));


                return articleServiceMock.Object;
            }
        }
    }
}
