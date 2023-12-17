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
            var sym = (char) stringReader.Read();
            return new Token(TokenType.Operation, sym.ToString());
        }
    }
}
