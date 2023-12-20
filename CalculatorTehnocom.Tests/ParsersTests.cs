using CalculatorTehnocom.Parsers;
using CalculatorTehnocom.Tokenizers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace CalculatorTehnocom.Tests
{
    [TestFixture]
    public class ParsersTests
    {
        private IServiceProvider _provider;

        public static IEnumerable<ITestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(new[]
                {
                    new Token(TokenType.Num, "11", ElementType.Num),
                    new Token(TokenType.Operation, "/", ElementType.Div),
                    new Token(TokenType.Num, "12", ElementType.Num)
                }, "1112/");

                yield return new TestCaseData(new[]
                {
                    new Token(TokenType.Num, "1", ElementType.Num),
                    new Token(TokenType.Operation, "+", ElementType.Plus),
                    new Token(TokenType.Num, "12.123", ElementType.FloatNum),
                    new Token(TokenType.Operation, "/", ElementType.Div),
                    new Token(TokenType.Num, "3", ElementType.Num),
                    new Token(TokenType.Operation, "-", ElementType.Minus),
                    new Token(TokenType.Brace, "(", ElementType.LBracket),
                    new Token(TokenType.Num, "1", ElementType.Num),
                    new Token(TokenType.Operation, "*", ElementType.Multiply),
                    new Token(TokenType.Num, "12", ElementType.Num),
                    new Token(TokenType.Operation, "!", ElementType.Fact),
                    new Token(TokenType.Operation, "%", ElementType.Percent),
                    new Token(TokenType.Num, "3.21", ElementType.FloatNum),
                    new Token(TokenType.Brace, ")", ElementType.RBracket)
                }, "112.1233/+112!*3.21%-");

                yield return new TestCaseData(new[]
                {
                    new Token(TokenType.Func, "cos", ElementType.Cos),
                    new Token(TokenType.Brace, "(", ElementType.LBracket),
                    new Token(TokenType.Num, "1", ElementType.Num),
                    new Token(TokenType.Brace, ")", ElementType.RBracket),
                    new Token(TokenType.Operation, "+", ElementType.Plus),
                    new Token(TokenType.Func, "sin", ElementType.Sin),
                    new Token(TokenType.Brace, "(", ElementType.LBracket),
                    new Token(TokenType.Num, "12.0001", ElementType.FloatNum),
                    new Token(TokenType.Brace, ")", ElementType.RBracket)
                }, "1cos12.0001sin+");
            }
        }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddParser();
            _provider = services.BuildServiceProvider();
        }

        [Test]
        public void SimpleMinus_RpnParser_Test()
        {
            var parser = _provider.GetRequiredService<IParser>();
            var input = new List<Token> 
            { 
                new Token(TokenType.Num, "1.001", ElementType.Num),
                new Token(TokenType.Operation, "-", ElementType.Minus),
                new Token(TokenType.Num, "1", ElementType.Num)
            };
            // Act
            parser.Parse(input.AsEnumerable());

            // Assert
            Assert.That(parser.ToString(), Is.EqualTo("1.0011-"));
        }

        [Test]
        [TestCaseSource(typeof(ParsersTests), nameof(TestCases))]
        public void Expression_Tokenize_Test(IEnumerable<Token> input, string expected)
        {
            var parser = _provider.GetRequiredService<IParser>();
            parser.Parse(input);

            CollectionAssert.AreEqual(expected, parser.ToString());
        }
    }
}
