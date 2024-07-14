namespace B3EvaluationProject.Api.Services
{
    public class InvestmentService : IInvestmentService
    {
        public decimal CDI { get; set; }
        public decimal TB { get; set; }

        public InvestmentService()
        {
            CDI = 0.9m; // CDI é 0.9% ao mês
            TB = 108m; // TB é 108%
        }

        public decimal CalculateGrossValue(decimal amountInvested, int months)
        {
            decimal monthlyYield = (TB / 100) * (CDI / 100);
            decimal grossValue = amountInvested * (decimal)Math.Pow((double)(1 + monthlyYield), months);
            return grossValue - amountInvested;
        }

        public decimal CalculateNetValue(decimal grossValue, int months)
        {
            decimal irRate = GetTaxRate(months);
            decimal ir = grossValue * irRate;
            decimal netValue = grossValue - ir;
            return netValue;
        }

        private static decimal GetTaxRate(int months)
        {
            if (months <= 6) return 0.225m;
            if (months <= 12) return 0.20m;
            if (months <= 24) return 0.175m;
            return 0.15m;
        }
    }
}
