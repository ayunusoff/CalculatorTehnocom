using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers.CalclulationNodes
{
    public class BraceNode : CalculationNode
    {
        public BraceNode(Token token) : base(token)
        {
            Priority = OperationPriority.None;
        }
    }
}
