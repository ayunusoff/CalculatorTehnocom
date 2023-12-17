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

            while (stringReader.Peek() != -1 && (char)stringReader.Peek() != ')')
            {
                valueToken.Append((char)stringReader.Read());
            }

            valueToken.Append((char)stringReader.Read());

            return new Token(TokenType.Func, valueToken.ToString());
        }
    }
}
