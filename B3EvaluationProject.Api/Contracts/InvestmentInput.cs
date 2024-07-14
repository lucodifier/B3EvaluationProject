using System.ComponentModel.DataAnnotations;

namespace B3EvaluationProject.Api.Contracts
{
    public class InvestmentInput
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor inicial deve ser maior que zero.")]
        public decimal InitialValue { get; set; }

        [Range(2, int.MaxValue, ErrorMessage = "O número de meses deve ser maior que (1) um.")]
        public int Months { get; set; }
    }

}
