using CalculatorTehnocom;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.RpnCalculation();
var provider = services.BuildServiceProvider();

var calc = provider.GetRequiredService<ICalculationContext>();

while (true)
{
    var str = Console.ReadLine();
    try
    {
        Console.WriteLine(calc.Eval(str));
    }
    catch (Exception e)
    {
        Console.WriteLine("Ошибка!!!");
        Console.WriteLine(new string('-', 100));
        Console.WriteLine(e);
        Console.WriteLine(new string('-', 100));
    }
}
