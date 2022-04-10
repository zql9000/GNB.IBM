using GNB.IBM.Application.Models;

namespace GNB.IBM.Application.Support
{
    public class CurrencyConversion
    {
        private readonly IEnumerable<ConversionRateModel> _conversions;

        public CurrencyConversion(IEnumerable<ConversionRateModel> conversions)
        {
            _conversions = conversions;
        }

        public float Convert(string from, string to, float amount)
        {
            if (from.Equals(to)) return amount;

            var conversionRate = _conversions.Where(conversion => 
                conversion.From.Equals(from) && conversion.To.Equals(to)).FirstOrDefault();

            if (conversionRate is not null)
                return GetRoundedConvertion(amount, conversionRate.Rate);

            return FindConversion(from, to, amount);
        }

        private float GetRoundedConvertion(float amount, float rate)
        {
            return (float)Math.Round(amount * rate, 2, MidpointRounding.ToEven);
        }

        private float FindConversion(string from, string to, float amount)
        {
            var visited = new HashSet<string>();
            var navigation = new Stack<ConversionRateModel>();
            var responsePath = new Stack<ConversionRateModel>();

            visited.Add(from);

            var filteredConversions = _conversions.Where(conversion => conversion.From.Equals(from));
            foreach (var conv in filteredConversions)
            {
                visited.Add(conv.To);
                navigation.Push(conv);
            }

            while (navigation.Any())
            {
                var currentNode = navigation.Pop();
                responsePath.Push(currentNode);
                if (currentNode.To.Equals(to))
                {
                    var pendingConversions = responsePath.ToArray();
                    var finalAmount = amount;
                    for (int i = pendingConversions.Length - 1; i >= 0; i--)
                    {
                        var res = pendingConversions[i];
                        finalAmount = GetRoundedConvertion(finalAmount, res.Rate);
                    }

                    return finalAmount;
                }

                filteredConversions = _conversions.Where(conv => 
                    conv.From.Equals(currentNode.To) && !visited.Contains(conv.To));

                if (!filteredConversions.Any()) responsePath.Pop();
                foreach (var conv in filteredConversions)
                {
                    visited.Add(conv.To);
                    navigation.Push(conv);
                }
            }

            throw new Exceptions.ApplicationException($"No existen conversiones para: {from} -> {to}.");
        }
    }
}
