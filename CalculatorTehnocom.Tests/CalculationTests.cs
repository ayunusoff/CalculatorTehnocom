using CalculatorTehnocom.Parsers.CalclulationNodes;
using CalculatorTehnocom.Tokenizers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace CalculatorTehnocom.Tests
{
    [TestFixture]
    public class CalculationTests
    {
        private IServiceProvider _provider;

        public static IEnumerable<ITestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("11/12", 11 / 12d);

                yield return new TestCaseData("cos(1)+sin(12.0001)", Math.Cos(1) + Math.Sin(12.0001));
                yield return new TestCaseData("1-(-10*1)", 11);
                yield return new TestCaseData("1+12.123/3-(1*12!%3.21)", 4.26);
            }
        }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.RpnCalculation();
            _provider = services.BuildServiceProvider();
        }

        [Test]
        [TestCaseSource(typeof(CalculationTests), nameof(TestCases))]
        public void Expression_Tokenize_Test(string str, double expected)
        {
            var context = _provider.GetRequiredService<ICalculationContext>();
            var actual = context.Eval(str);

            Assert.That(Math.Round(actual, 2), Is.EqualTo(Math.Round(expected, 2)));
        }

    }
}
