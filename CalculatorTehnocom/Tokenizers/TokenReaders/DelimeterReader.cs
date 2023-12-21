namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public class DelimeterReader : ITokenReader
    {
        public bool CanRead(char sym)
            => sym == ',';

        public Token Read(StringReader stringReader)
        {
            var sym = ((char)stringReader.Read()).ToString();
            return new Token(TokenType.Delimeter, sym, ElementType.Delimeter);
        }
    }
}
