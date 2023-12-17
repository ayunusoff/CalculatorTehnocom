namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public class BraceReader : ITokenReader
    {
        public bool CanRead(char sym)
            => sym == '(' || sym == ')';

        public Token Read(StringReader stringReader)
        {
            var sym = ((char)stringReader.Read()).ToString();

            var elementType = sym switch
            {
                "(" => ElementType.LBracket,
                ")" => ElementType.RBracket,
                _ => throw new Exception($"{sym}")
            };

            return new Token(TokenType.Brace, sym, elementType);
        }
    }
}
