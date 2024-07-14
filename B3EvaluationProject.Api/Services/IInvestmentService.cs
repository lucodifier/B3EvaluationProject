namespace B3EvaluationProject.Api.Services
{
    public interface IInvestmentService
    {
        public decimal CalculateGrossValue(decimal amountInvested, int months);

        public decimal CalculateNetValue(decimal grossValue, int months);
    }
}
