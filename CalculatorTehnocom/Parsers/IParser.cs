using CalculatorTehnocom.Parsers.CalclulationNodes;
using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers
{
    public interface IParser
    {
        IEnumerable<CalculationNode> Parse(IEnumerable<Token> tokens);
    }
}
