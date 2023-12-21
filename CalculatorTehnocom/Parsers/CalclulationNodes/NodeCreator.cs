using CalculatorTehnocom.Tokenizers;

namespace CalculatorTehnocom.Parsers.CalclulationNodes
{
    internal static class NodeCreator
    {
        public static CalculationNode Create(Token token)
            => token.Type switch
            {
                TokenType.Num => new NumNode(token),
                TokenType.Operation => new OperationNode(token),
                TokenType.Brace => new BraceNode(token),
                TokenType.Func => new FuncNode(token),
                TokenType.Delimeter => new DelimeterNode(token)
            };
    }
}
