using System.Text;

namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public class NumReader : ITokenReader
    {
        public bool CanRead(char sym) => char.IsDigit(sym);

        public Token Read(StringReader stringReader)
        {
            var valueToken = new StringBuilder();

            while (stringReader.Peek() != -1 && 
                (CanRead((char)stringReader.Peek()) || IsPunctuation((char)stringReader.Peek())))
            {
                valueToken.Append((char)stringReader.Read());
            }

            var valueTokenStr = valueToken.ToString();

            var elementType = valueTokenStr.Any(IsPunctuation) ? ElementType.FloatNum : ElementType.Num;

            return new Token(TokenType.Num, valueTokenStr, elementType);
        }

        private bool IsPunctuation(char v)
            => v == '.';
    }
}
