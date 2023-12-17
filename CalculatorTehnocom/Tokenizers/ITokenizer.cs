namespace CalculatorTehnocom.Tokenizers
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(string expression);
    }
}
