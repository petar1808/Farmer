using Application.Models.ArableLands;
using Application.Services.ArableLands;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.ViewModels.ArableLands;

namespace Farmer.Test.Mocks
{
    public class ArableLandServiceMock
    {
        public static IArableLandService InstanceEdit
        {
            get
            {
                var arableLandServiceMock = new Mock<IArableLandService>();

                arableLandServiceMock
                    .Setup(a => a.Edit(1, "voduema", 60));
                    

                return arableLandServiceMock.Object;
            }
        }

        public static IArableLandService InstanceDelete
        {
            get
            {
                var arableLandServiceMock = new Mock<IArableLandService>();

                arableLandServiceMock
                    .Setup(a => a.Delete(1));


                return arableLandServiceMock.Object;
            }
        }

        public static IArableLandService InstanceGet
        {
            get
            {
                var arableLandServiceMock = new Mock<IArableLandService>();

                arableLandServiceMock
                    .Setup(a => a.Get(1));


                return arableLandServiceMock.Object;
            }
        }


        public static IArableLandService InstanceAdd
        {
            get
            {
                var arableLandServiceMock = new Mock<IArableLandService>();

                arableLandServiceMock
                    .Setup(a => a.Add("qzovira",50));


                return arableLandServiceMock.Object;
            }
        }
    }
}
