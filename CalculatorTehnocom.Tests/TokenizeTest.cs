using CalculatorTehnocom.Tokenizers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace CalculatorTehnocom.Tests
{
    [TestFixture]
    public class TokenizeTests
    {
        private IServiceProvider _provider;

        public static IEnumerable<ITestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("11/12", new[]
                {
                    new Token(TokenType.Num, "11", ElementType.Num),
                    new Token(TokenType.Operation, "/", ElementType.Div),
                    new Token(TokenType.Num, "12", ElementType.Num)
                });

                yield return new TestCaseData("1-1", new[]
                {
                    new Token(TokenType.Num, "1", ElementType.Num),
                    new Token(TokenType.Operation, "-", ElementType.Minus),
                    new Token(TokenType.Num, "1", ElementType.Num)
                });

                yield return new TestCaseData("1+12.123/3-(1*12!%3.21)", new[]
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
                    new Token(TokenType.Func, "!", ElementType.Fact),
                    new Token(TokenType.Operation, "%", ElementType.Percent),
                    new Token(TokenType.Num, "3.21", ElementType.FloatNum),
                    new Token(TokenType.Brace, ")", ElementType.RBracket)

                });

                yield return new TestCaseData("cos(1)+sin(12.0001)", new[]
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
                });
            }
        }


        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddTokenizer();
            _provider = services.BuildServiceProvider();
        }

        [Test]
        public void Float_Num_Tokenize_Test()
        {
            var tokenizer = _provider.GetRequiredService<ITokenizer>();
            // Act
            var token = tokenizer.Tokenize("1.001").First();

            // Assert
            Assert.That(token.Type, Is.EqualTo(TokenType.Num));
            Assert.That(token.Value, Is.EqualTo("1.001"));
        }
        
        [Test]
        public void Math_Op_Tokenize_Test()
        {
            var tokenizer = _provider.GetRequiredService<ITokenizer>();
            // Act
            var token = tokenizer.Tokenize("-").First();

            // Assert
            Assert.That(token.Type, Is.EqualTo(TokenType.Operation));
            Assert.That(token.Value, Is.EqualTo("-"));
        }

        [Test]
        public void Func_Tokenize_Test()
        {
            var tokenizer = _provider.GetRequiredService<ITokenizer>();
            // Act
            var token = tokenizer.Tokenize("tg(12)").First();

            // Assert
            Assert.That(token.Type, Is.EqualTo(TokenType.Func));
            Assert.That(token.Value, Is.EqualTo("tg"));
        }

        [Test]
        public void Bracket_Tokenize_Test()
        {
            var tokenizer = _provider.GetRequiredService<ITokenizer>();
            // Act
            var token = tokenizer.Tokenize("()").First();

            // Assert
            Assert.That(token.Type, Is.EqualTo(TokenType.Brace));
            Assert.That(token.Value, Is.EqualTo("("));
        }

        [Test]
        [TestCaseSource(typeof(TokenizeTests), nameof(TestCases))]
        public void Expression_Tokenize_Test(string exprs, IEnumerable<Token> expected)
        {
            var tokenizer = _provider.GetRequiredService<ITokenizer>();
            var actual = tokenizer.Tokenize(exprs);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}