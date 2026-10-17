using CalculateAmounts.Shopping;
using System;

namespace CalculateAmounts
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("CalculateAmounts library - run tests to verify behavior \"dotnet test\".");

            BasketCalculator basket = new BasketCalculator();
            var items = new string[] { "Apple", "Banana", "Melon", "Lime" };
            var total = basket.CalculateTotal(items);
            Console.WriteLine($"Total: {total}");



        }
    }
}
