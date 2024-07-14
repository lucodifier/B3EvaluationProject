using B3EvaluationProject.Api.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace B3EvaluationProject.Tests.Integration
{
    public class InvestmentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public InvestmentControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Calculate_ShouldReturnOkResult_WithValidInput()
        {
            // Arrange
            var input = new InvestmentInput
            {
                InitialValue = 10000m,
                Months = 12
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Investment", input);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<InvestmentResult>();
            Assert.NotNull(result);

            // Valor esperado calculado manualmente
            decimal expectedGrossValue = 1230.82m; // valor esperado fixo
            decimal expectedNetValue = 984.66m; // valor esperado fixo considerando imposto de 20%

            // Comparar valores
            Assert.Equal(expectedGrossValue, result.GrossValue, precision: 2);
            Assert.Equal(expectedNetValue, result.NetValue, precision: 2);
        }

        [Fact]
        public async Task Calculate_ShouldReturnCorrectTaxRate()
        {
            // Test for 6 months
            var input1 = new InvestmentInput
            {
                InitialValue = 1000m,
                Months = 6
            };
            var response1 = await _client.PostAsJsonAsync("/api/Investment", input1);
            response1.EnsureSuccessStatusCode();
            var result1 = await response1.Content.ReadFromJsonAsync<InvestmentResult>();
            Assert.NotNull(result1);
            Assert.Equal(result1.GrossValue * (1 - 0.225m), result1.NetValue, precision: 2);

            // Test for 12 months
            var input2 = new InvestmentInput
            {
                InitialValue = 1000m,
                Months = 12
            };
            var response2 = await _client.PostAsJsonAsync("/api/Investment", input2);
            response2.EnsureSuccessStatusCode();
            var result2 = await response2.Content.ReadFromJsonAsync<InvestmentResult>();
            Assert.NotNull(result2);
            Assert.Equal(result2.GrossValue * (1 - 0.20m), result2.NetValue, precision: 2);

            // Test for 24 months
            var input3 = new InvestmentInput
            {
                InitialValue = 1000m,
                Months = 24
            };
            var response3 = await _client.PostAsJsonAsync("/api/Investment", input3);
            response3.EnsureSuccessStatusCode();
            var result3 = await response3.Content.ReadFromJsonAsync<InvestmentResult>();
            Assert.NotNull(result3);
            Assert.Equal(result3.GrossValue * (1 - 0.175m), result3.NetValue, precision: 2);

            // Test for 36 months
            var input4 = new InvestmentInput
            {
                InitialValue = 1000m,
                Months = 36
            };
            var response4 = await _client.PostAsJsonAsync("/api/Investment", input4);
            response4.EnsureSuccessStatusCode();
            var result4 = await response4.Content.ReadFromJsonAsync<InvestmentResult>();
            Assert.NotNull(result4);
            Assert.Equal(result4.GrossValue * (1 - 0.15m), result4.NetValue, precision: 2);
        }
    }
}
