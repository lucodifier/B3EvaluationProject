using B3EvaluationProject.Api.Contracts;
using B3EvaluationProject.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace B3EvaluationProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;
        public InvestmentController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpPost]
        public ActionResult<InvestmentResult> Calculate(InvestmentInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            decimal grossValue = _investmentService.CalculateGrossValue(input.InitialValue, input.Months);
            decimal netValue = _investmentService.CalculateNetValue(grossValue, input.Months);

            return Ok(new InvestmentResult
            {
                GrossValue = grossValue,
                NetValue = netValue,
                TotalGrossValue = grossValue + input.InitialValue,
                TotalNetValue = netValue + input.InitialValue
            });
        }
    }
}