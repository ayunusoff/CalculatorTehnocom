using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers
{
    public interface IParser
    {
        void Parse(IEnumerable<Token> tokens);
    }
}
