using System.Collections.Generic;
using System.Linq;
using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Extensions
{
    public static class ColorExtensions
    {
        private static readonly Dictionary<Color, int> ColorIndexMapping;

        static ColorExtensions()
        {
            ColorIndexMapping = GetColorIndexMapping();
        }

        public static int Index(this Color color)
        {
            return ColorIndexMapping[color];
        }

        public static bool Matches(this Color color, IList<Color> colors)
        {
            return !colors.Any() || colors.Select(c => c.Id).Contains(color.Id);
        }

        private static Dictionary<Color, int> GetColorIndexMapping()
        {
            return Color.All
                .Select((c, i) => new { Index = i, Color = c })
                .ToDictionary(ci => ci.Color, ci => ci.Index);
        }
    }
}
