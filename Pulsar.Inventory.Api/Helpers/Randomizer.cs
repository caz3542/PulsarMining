using System;
using System.Text;

namespace Pulsar.Inventory.Api.Helpers
{
    public static class Randomizer
    {
        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString().ToUpper();
        }
    }
}