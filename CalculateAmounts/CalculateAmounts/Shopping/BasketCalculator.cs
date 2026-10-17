using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculateAmounts.Shopping
{
    public class BasketCalculator
    {
        private static readonly Dictionary<string,int> Prices = new(StringComparer.OrdinalIgnoreCase)
        {
            {"Apple", 35},
            {"Banana", 20},
            {"Melon", 50},
            {"Lime", 15}
        };
        public int CalculateTotal(IEnumerable<string> items)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));

            var list = items.ToList();
            if (list.Count == 0) return 0;

            var counts = new Dictionary<string,int>(StringComparer.OrdinalIgnoreCase);

            foreach (var entry in list)
            {
                if (entry is null) throw new ArgumentException("Item names must not contain null entries.", nameof(items));

                var name = entry.Trim();
                if (!Prices.ContainsKey(name)) throw new ArgumentException($"Unknown item: '{entry}'", nameof(items));

                counts.TryGetValue(name, out var c);
                counts[name] = c + 1;
            }

            var total = 0;

            if (counts.TryGetValue("Apple", out var apples))
            {
                total += apples * Prices["Apple"];
            }

            if (counts.TryGetValue("Banana", out var bananas))
            {
                total += bananas * Prices["Banana"];
            }

            if (counts.TryGetValue("Melon", out var melons))
            {
                var chargeable = (melons + 1) / 2;
                total += chargeable * Prices["Melon"];
            }
            if (counts.TryGetValue("Lime", out var limes))
            {
                var groupsOfThree = limes / 3;
                var remainder = limes % 3;
                var chargedUnits = groupsOfThree * 2 + remainder;
                total += chargedUnits * Prices["Lime"];
            }

            return total;
        }
    }
}
