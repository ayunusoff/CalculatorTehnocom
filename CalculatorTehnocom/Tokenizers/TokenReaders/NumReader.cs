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

            return new Token(TokenType.Num, valueToken.ToString());
        }

        private bool IsPunctuation(char v)
            => v == '.' || v == ',';
    }
}
