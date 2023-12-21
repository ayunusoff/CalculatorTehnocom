using CalculatorTehnocom.Parsers.CalclulationNodes;
using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers
{
    public class RpnParser : IParser
    {
        private List<CalculationNode> _output;

        public List<CalculationNode> Output { get => _output; }
        public RpnParser()
        {
            _output = new List<CalculationNode>();
        }

        public IEnumerable<CalculationNode> Parse(IEnumerable<Token> tokens)
        {
            var nodes = tokens.Select(x => NodeCreator.Create(x));

            var stack = new Stack<CalculationNode>();
            var prevNode = NodeCreator.Create(new Token(TokenType.Operation, ""));
            foreach (var node in nodes)
            {
                switch (node) 
                {
                    case NumNode:
                        _output.Add(node);
                        break;
                    case BraceNode:
                        if (node.ElementType == ElementType.LBracket)
                            stack.Push(node);
                        else
                        {
                            while (stack.Peek().ElementType != ElementType.LBracket)
                                _output.Add(stack.Pop());
                            stack.Pop();
                        }
                        break;
                    case OperationNode:
                        if (prevNode is { Value: "" } || prevNode is { ElementType: ElementType.LBracket }) // проверка на унарность
                        {
                            node.Arity = ArityType.Unary;
                            node.Priority = OperationPriority.Absolut;
                            stack.Push(node);
                        }
                        else if (stack.Count() == 0)
                            stack.Push(node);
                        else
                        {
                            while (stack.Count() > 0
                                && (stack.Peek() is FuncNode { Position: OperationPosition.Prefix }
                                || stack.Peek().Priority >= node.Priority))
                                _output.Add(stack.Pop());
                            stack.Push(node);
                        }
                        break;
                    case FuncNode:
                        if (node.Position == OperationPosition.Postfix)
                            _output.Add(node);
                        else
                            stack.Push(node);
                        break;
                    case DelimeterNode:
                        while (stack.Count() > 0 && stack.Peek().ElementType != ElementType.LBracket)
                            _output.Add(stack.Pop());
                        break;
                }
                prevNode = node;
            }
            while (stack.Count() > 0)
                _output.Add(stack.Pop());

            return _output;
        }

        public override string ToString()
        {
            return string.Join("", _output.Select(x => x.Value));
        }
    }
}
