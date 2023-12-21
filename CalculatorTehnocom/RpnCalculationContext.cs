using CalculatorTehnocom.Parsers;
using CalculatorTehnocom.Parsers.CalclulationNodes;
using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;
using System.Globalization;

namespace CalculatorTehnocom
{
    public class RpnCalculationContext : ICalculationContext
    {
        private ITokenizer _tokenizer;
        private IParser _parser;

        public RpnCalculationContext(IParser parser, ITokenizer tokenizer)
        {
            _parser = parser;
            _tokenizer = tokenizer;
        }

        public double Eval(string expr)
        {
            var tokens = _tokenizer.Tokenize(expr);

            var stack = new Stack<double>();

            foreach (var node in _parser.Parse(tokens))
            {
                switch (node)
                {
                    case NumNode:
                        stack.Push(double.Parse(node.Value, CultureInfo.InvariantCulture));
                        break;
                    case OperationNode:
                        var opr = ResolveOperation(node);
                        stack.Push(opr(stack));
                        break;
                    case FuncNode:
                        var func = ResolveFunc(node);
                        stack.Push(func(stack));
                        break;
                        
                }
            }
            return stack.Pop();
        }

        private Func<Stack<double>, double> ResolveOperation(CalculationNode node)
            => (node.ElementType, node.Arity) switch
            {
                (ElementType.Multiply, ArityType.Binary) => x => x.Pop() * x.Pop(),
                (ElementType.Div, ArityType.Binary) => 
                    x => { 
                        var k = x.Pop();
                        var y = x.Pop();
                        return y / k; 
                    },
                (ElementType.Minus, ArityType.Binary) =>
                    x => {
                        var k = x.Pop();
                        var y = x.Pop();
                        return y - k;
                    },
                (ElementType.Plus, ArityType.Binary) => x => x.Pop() + x.Pop(),
                (ElementType.Percent, ArityType.Binary) => 
                    x => {
                        var k = x.Pop();
                        var y = x.Pop();
                        return y % k;
                    },
                (ElementType.Minus, ArityType.Unary) => x => - x.Pop(),
                _ => throw new Exception()
            };

        private Func<Stack<double>, double> ResolveFunc(CalculationNode node)
            => node.ElementType switch
            {
                ElementType.Sin => x => Math.Sin(x.Pop()),
                ElementType.Cos => x => Math.Cos(x.Pop()),
                ElementType.Tg => x => Math.Tan(x.Pop()),
                ElementType.Ctg => x => 1 / Math.Tan(x.Pop()),
                ElementType.Pow => x => Math.Pow(x.Pop(), x.Pop()),
                ElementType.Fact => x => Enumerable.Range(1, Convert.ToInt32(x.Pop())).Aggregate((x, y) => x * y)
            };
    }
}
