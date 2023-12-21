using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom
{
    public interface ICalculationContext
    {
        double Eval(string expr);
    }
}
