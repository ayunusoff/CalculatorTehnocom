using CalculatorTehnocom.Parsers.CalclulationNodes.Enums;
using CalculatorTehnocom.Tokenizers;
using System.Data;

namespace CalculatorTehnocom.Parsers.CalclulationNodes
{
    public abstract class CalculationNode
    {
        public OperationPriority Priority { get; set; } = OperationPriority.None;
        public ArityType? Arity { get; set; }
        public OperationPosition? Position { get; protected set; }
        public ElementType ElementType { get => token.ElementType; }
        public CalculationNode(Token token)
        {
            this.token = token;
            Value = token.Value;
        }
        protected string value;

        public string Value
        {
            get => token.Value;
            protected set => this.value = value;
        }

        protected Token token;
    }
}
