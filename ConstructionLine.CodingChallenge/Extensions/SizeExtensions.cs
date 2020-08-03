using System.Collections.Generic;
using System.Linq;
using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Extensions
{
    public static class SizeExtensions
    {
        private static readonly Dictionary<Size, int> SizeIndexMapping;

        static SizeExtensions()
        {
            SizeIndexMapping = GetSizeIndexMapping();
        }

        public static int Index(this Size size)
        {
            return SizeIndexMapping[size];
        }
        public static bool Matches(this Size size, IList<Size> sizes)
        {
            return !sizes.Any() || sizes.Select(c => c.Id).Contains(size.Id);
        }

        private static Dictionary<Size, int> GetSizeIndexMapping()
        {
            return Size.All
                .Select((s, i) => new { Index = i, Size = s })
                .ToDictionary(si => si.Size, si => si.Index);
        }
    }
}
