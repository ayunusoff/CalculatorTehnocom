using System.Text;

namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public class OperationReader : ITokenReader
    {
        public bool CanRead(char sym)
            => IsMathOp(sym);

        private bool IsMathOp(char sym) => sym == '*' 
            || sym == '/' || sym == '%' 
            || sym == '+' || sym == '-'
            || sym == '!';

        public Token Read(StringReader stringReader)
        {
            var sym = ((char)stringReader.Read()).ToString();

            var elementType = sym switch
            {
                "*" => ElementType.Multiply,
                "/" => ElementType.Div,
                "+" => ElementType.Plus,
                "-" => ElementType.Minus,
                "!" => ElementType.Fact,
                "%" => ElementType.Percent,
                _ => throw new Exception($"{sym}")
            };

            return new Token(TokenType.Operation, sym, elementType);
        }
    }
}
