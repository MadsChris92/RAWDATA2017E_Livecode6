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
            var ctrl = new CategoriesController();
        }
    }
}
