using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers
{
    public class RpnParser : IParser
    {
        private List<Token> _output;

        public List<Token> Output { get => _output; }
        public RpnParser()
        {
            _output = new List<Token>();
        }

        public void Parse(IEnumerable<Token> tokens)
        {
            var stack = new Stack<Token>();
            var prevToken = new Token(TokenType.Num, "");
            foreach (var token in tokens)
            {
                switch (token.Type) 
                {
                    case TokenType.Num:
                        _output.Add(token);
                        break;
                    case TokenType.Brace:
                        if (token.ElementType == ElementType.LBracket)
                            stack.Push(token);
                        else
                        {
                            while (stack.Peek().ElementType != ElementType.LBracket)
                            {
                                _output.Add(stack.Pop());
                            }
                            stack.Pop();
                        }
                        break;
                    case TokenType.Operation:
                        if (token.ElementType == ElementType.Fact)
                            _output.Add(token);
                        else if (stack.Count() == 0)
                            stack.Push(token);
                        else
                        {
                            while (stack.Count() > 0 && stack.Peek().Type != TokenType.Brace 
                                && (stack.Peek().Type == TokenType.Func
                                    || stack.Peek().ElementType >= token.ElementType
                                    || (stack.Peek().ElementType == token.ElementType && token.Value != "+")))
                                _output.Add(stack.Pop());
                            stack.Push(token);
                        }
                        break;
                    case TokenType.Func:
                        stack.Push(token);
                        break;
                }
            }
            while (stack.Count() > 0)
                _output.Add(stack.Pop());
        }

        public override string ToString()
        {
            return string.Join("", _output.Select(x => x.Value));
        }
    }
}
