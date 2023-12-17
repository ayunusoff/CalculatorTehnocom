using System.Text;

namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public class FuncReader : ITokenReader
    {
        public bool CanRead(char sym)
            => char.IsLetter(sym);

        public Token Read(StringReader stringReader)
        {
            var valueToken = new StringBuilder();

            while (stringReader.Peek() != -1 && (char)stringReader.Peek() != '(' && CanRead((char)stringReader.Peek()))
            {
                valueToken.Append((char)stringReader.Read());
            }

            var valueTokenStr = valueToken.ToString();

            var elementType = valueTokenStr switch
            {
                "pow" => ElementType.Pow,
                "sin" => ElementType.Sin,
                "cos" => ElementType.Cos,
                "tan" or "tg" => ElementType.Tg,
                "ctg" or "cot" => ElementType.Ctg,
                _ => throw new NotSupportedException($"Функия {valueTokenStr} пока что не работает")
            };

            return new Token(TokenType.Func, valueToken.ToString(), elementType);
        }
    }
}
