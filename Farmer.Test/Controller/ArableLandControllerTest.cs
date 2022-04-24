using AutoMapper;
using Farmer.Test.Mocks;
using Infrastructure.DbContect;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Web.Controllers;
using Web.ViewModels.ArableLands;
using Xunit;

namespace Farmer.Test.Controller
{
    public class ArableLandControllerTest
    {
        [Fact]
        public void AddShouldReturnView()
        {
            //Arrange
            var arableLandController = new ArableLandsController(null!, null!);
            //Act
            var result = arableLandController.Add();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddShouldReturnRedirectToAction()
        {
            //Arrange
            var arableLandController = new ArableLandsController(ArableLandServiceMock.InstanceAdd, null!);
            //Act
            var result = await arableLandController.Add(new AddArableLandViewModel
            {
                Name = "qzovira",
                SizeInDecar = 50
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ArableLands", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task EditShouldReturnBadRequest()
        {
            //Arrange
            var arableLandController = new ArableLandsController(null!, null!);
            //Act
            var result = await arableLandController.Edit(null!);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnViewModel()
        {
            //Arrange
            var arableLandController = new ArableLandsController(ArableLandServiceMock.InstanceGet, Mock.Of<IMapper>());
            //Act
            var result = await arableLandController.Edit(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToAction()
        {
            //Arrange
            var arableLandController = new ArableLandsController(ArableLandServiceMock.InstanceEdit, null!);
            //Act
            var result = await arableLandController.Edit(new EditArableLandViewModel
            {
                Id = 1,
                Name = "qzovira",
                SizeInDecar = 50
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);
           
            Assert.Equal("ArableLands", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task DeleteShouldReturnRedirectToAction()
        {
            //Arrange
            var arableLandController = new ArableLandsController(ArableLandServiceMock.InstanceDelete, null!);
            //Act
            var result = await arableLandController.Delete(1);

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("ArableLands", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }
    }
}
