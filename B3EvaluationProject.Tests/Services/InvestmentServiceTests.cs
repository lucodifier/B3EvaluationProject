using B3EvaluationProject.Api.Services;
using Xunit;

namespace B3EvaluationProject.Tests.Services
{
    public class InvestmentServiceTests
    {
        private readonly InvestmentService _investmentService;

        public InvestmentServiceTests()
        {
            _investmentService = new InvestmentService();
        }

        [Theory]
        [InlineData(10000, 12, 108, 0.9, 1230.82)]
        [InlineData(10000, 6, 108, 0.9, 597.556)]
        [InlineData(10000, 24, 108, 0.9, 2613.13)]
        [InlineData(10000, 36, 108, 0.9, 4165.5848)]
        public void CalculateGrossValue_ShouldCalculateCorrectly(decimal amountInvested, int months, decimal tb, decimal cdi, decimal expected)
        {
            // Arrange
            _investmentService.TB = tb;
            _investmentService.CDI = cdi;

            // Act
            var result = _investmentService.CalculateGrossValue(amountInvested, months);

            // Assert
            Assert.Equal(expected, result, 2);
        }

        [Theory]
        [InlineData(1230.82, 12, 984.656)]
        [InlineData(597.556, 6, 463.1059)]
        [InlineData(2613.13, 24, 2155.83225)]
        [InlineData(4165.5848, 36, 3540.747)]
        public void CalculateNetValue_ShouldCalculateCorrectly(decimal grossValue, int months, decimal expectedNet)
        {
            // Arrange
            _investmentService.TB = 108m;
            _investmentService.CDI = 0.9m;

            // Act
            var result = _investmentService.CalculateNetValue(grossValue, months);

            // Assert
            Assert.Equal(expectedNet, result, 2);
        }
    }
}
