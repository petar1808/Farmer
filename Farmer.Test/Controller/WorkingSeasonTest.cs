using AutoMapper;
using FarmerWeb.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Web.Controllers;
using Web.ViewModels.WorkingSeasons;
using Xunit;

namespace FarmerWeb.Test.Controller
{
    public class WorkingSeasonTest
    {
        [Fact]
        public void AddShouldReturnView()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(null!, null!);
            //Act
            var result = workingSeasonController.Add();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddShouldReturnRedirectToAction()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(WorkingSeasonServiceMock.InstanceAdd, null!);
            //Act
            var result = await workingSeasonController.Add(new AddWorkingSeasonModel
            {
                Name = "2021/2022",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1)
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("WorkingSeasons", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task EditShouldReturnBadRequest()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(null!, null!);
            //Act
            var result = await workingSeasonController.Edit(null!);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnViewModel()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(WorkingSeasonServiceMock.InstanceGet, Mock.Of<IMapper>());
            //Act
            var result = await workingSeasonController.Edit(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToAction()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(WorkingSeasonServiceMock.InstanceEdit, null!);
            //Act
            var result = await workingSeasonController.Edit(new EditWorkingSeasonModel
            {
                Name = "2022/2023",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(1)
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("WorkingSeasons", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task DeleteShouldReturnRedirectToAction()
        {
            //Arrange
            var workingSeasonController = new WorkingSeasonsController(WorkingSeasonServiceMock.InstanceDelete, null!);
            //Act
            var result = await workingSeasonController.Delete(1);

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("WorkingSeasons", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }
    }
}
