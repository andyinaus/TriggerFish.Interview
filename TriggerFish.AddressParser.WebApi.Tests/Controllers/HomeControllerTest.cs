using System.Web.Mvc;
using Shouldly;
using TriggerFish.AddressParser.WebApi.Controllers;
using Xunit;

namespace TriggerFish.AddressParser.WebApi.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            result.ShouldNotBeNull();
        }
    }
}
