using AutoMapper;
using Domain.Enum;
using FarmerWeb.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Web.ViewModels.Articles;
using Xunit;

namespace FarmerWeb.Test.Controller
{
    public class ArticleControllerTest
    {
        [Fact]
        public void AddShouldReturnView()
        {
            //Arrange
            var articleController = new ArticlesController(null!, null!);
            //Act
            var result = articleController.Add();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddShouldReturnRedirectToAction()
        {
            //Arrange
            var articleController = new ArticlesController(ArticleServiceMock.InstanceAdd, null!);
            //Act
            var result = await articleController.Add(new AddArticleViewModel
            {
                Name = "пшеница",
                ArticleType = ArticleType.Seeds
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Articles", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);
        }


        [Fact]
        public async Task EditShouldReturnBadRequest()
        {
            //Arrange
            var articleController = new ArticlesController(null!, null!);
            //Act
            var result = await articleController.Edit(null!);

            //Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnViewModel()
        {
            //Arrange
            var articleController = new ArticlesController(ArticleServiceMock.InstanceGet, Mock.Of<IMapper>());
            //Act
            var result = await articleController.Edit(1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditShouldReturnRedirectToAction()
        {
            //Arrange
            var articleController = new ArticlesController(ArticleServiceMock.InstanceEdit, null!);
            //Act
            var result = await articleController.Edit(new EditArticleViewModel
            {
                Id = 1,
                Name = "пшеница",
                ArticleType = ArticleType.Seeds
            }) as RedirectToActionResult;

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Articles", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }

        [Fact]
        public async Task DeleteShouldReturnRedirectToAction()
        {
            //Arrange
            var articleController = new ArticlesController(ArticleServiceMock.InstanceDelete, null!);
            //Act
            var result = await articleController.Delete(1);

            //Assert
            var redirectToActionResult =
            Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Articles", redirectToActionResult.ControllerName);
            Assert.Equal("All", redirectToActionResult.ActionName);

        }
    }
}
