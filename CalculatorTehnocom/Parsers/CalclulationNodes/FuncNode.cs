using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;
using System.Dynamic;

namespace CalculatorTehnocom.Parsers.CalclulationNodes
{
    public class FuncNode : CalculationNode
    {
        public FuncNode(Token token) : base(token)
        {
            Priority = OperationPriority.High;
            Position = GetPosition(token);
            Arity = GetArity(token);
        }

        private OperationPosition GetPosition(Token token)
            => token.ElementType switch
            {
                ElementType.Fact => OperationPosition.Postfix,
                _ => OperationPosition.Prefix,
            };

        private ArityType GetArity(Token token)
            => token.ElementType switch
            {
                ElementType.Pow => ArityType.Binary,
                _ => ArityType.Unary,
            };
    }
}
