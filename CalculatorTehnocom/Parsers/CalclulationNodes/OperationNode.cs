using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers.CalclulationNodes
{
    public class OperationNode : CalculationNode
    {
        public OperationNode(Token token) : base(token)
        {
            Priority = GetOperationPriority(token);
            Position = GetOperationPosition(token);
            Arity = ArityType.Binary;
        }

        private OperationPriority GetOperationPriority(Token token)
            => token.ElementType switch
            {
                ElementType.Plus or ElementType.Minus => OperationPriority.Low,
                ElementType.Multiply or ElementType.Div or ElementType.Percent => OperationPriority.Middle,
                ElementType.Fact => OperationPriority.High,
                _ => OperationPriority.None,
            };

        private OperationPosition GetOperationPosition(Token token)
            => token.ElementType switch
            {
                ElementType.Plus or ElementType.Minus or ElementType.Multiply or ElementType.Div or ElementType.Percent => OperationPosition.Infix,
                ElementType.Fact => OperationPosition.Postfix,
                _ => OperationPosition.None,
            };
    }
}
