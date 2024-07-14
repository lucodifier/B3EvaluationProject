using B3EvaluationProject.Api.Contracts;
using B3EvaluationProject.Api.Controllers;
using B3EvaluationProject.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace B3EvaluationProject.Tests.Controllers
{
    public class InvestmentControllerTests
    {
        private readonly InvestmentController _controller;
        private readonly IInvestmentService _investmentService;

        public InvestmentControllerTests()
        {
            _investmentService = new InvestmentService();
            _controller = new InvestmentController(_investmentService);
        }

        [Fact]
        public void Calculate_ShouldReturnCorrectValues()
        {
            // Arrange
            var input = new InvestmentInput { InitialValue = 10000, Months = 12 };

            // Act
            var result = _controller.Calculate(input);
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var investmentResult = okResult.Value as InvestmentResult;
            Assert.NotNull(investmentResult);

            // Define a tolerance for comparison
            decimal tolerance = 0.01m;

            // Compare values with tolerance
            Assert.True(Math.Abs(investmentResult.GrossValue - 1230.820m) < tolerance, $"Expected: 1230.820, Actual: {investmentResult.GrossValue}");
            Assert.True(Math.Abs(investmentResult.NetValue - 984.656m) < tolerance, $"Expected: 984.656, Actual: {investmentResult.NetValue}");
        }

        [Fact]
        public void Calculate_InvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("InitialValue", "Required");

            // Act
            var result = _controller.Calculate(new InvestmentInput());

            var badResult = result.Result as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResult);
        }
    }
}
