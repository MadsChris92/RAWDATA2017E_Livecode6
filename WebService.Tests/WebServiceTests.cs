using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebService.Controllers;
using Xunit;

namespace WebService.Tests
{
    public class WebServiceTests
    {
        [Fact]
        public void GetCategory_ValidId_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock.Setup(o => o.GetCategory(It.IsAny<int>())).Returns(new Category());
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new CategoriesController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.GetCategory(2);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void GetCategory_InvalidId_ReturnsNotFund()
        {
            var dataServviceMock = new Mock<IDataService>();

            var ctrl = new CategoriesController(dataServviceMock.Object);

            var response = ctrl.GetCategory(-1);

            Assert.IsType<NotFoundResult>(response);
        }
    }
}
