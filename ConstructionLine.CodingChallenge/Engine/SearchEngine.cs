using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Caches;
using ConstructionLine.CodingChallenge.Extensions;
using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Engine
{
    public class SearchEngine
    {
        private readonly ShirtsByColorAndSizeCache _shirtsByColorAndSizeCache;
        private readonly NumberShirtsByColorAndSizeCache _numberShirtsByColorAndSizeCache;

        public SearchEngine(IEnumerable<Shirt> shirts)
        {
            _shirtsByColorAndSizeCache = new ShirtsByColorAndSizeCache(Color.All.Count, Size.All.Count);
            _numberShirtsByColorAndSizeCache = new NumberShirtsByColorAndSizeCache(_shirtsByColorAndSizeCache);

            Initialize(shirts);
        }

        public SearchResults Search(SearchOptions searchOptions)
        {
            return new SearchResults
            {
                Shirts = GetMatchingShirts(searchOptions),
                ColorCounts = GetColorCounts(searchOptions),
                SizeCounts = GetSizeCounts(searchOptions)
            };
        }

        private void Initialize(IEnumerable<Shirt> shirts)
        {
            _shirtsByColorAndSizeCache.Init(shirts);
            _numberShirtsByColorAndSizeCache.InitFromCache();
        }

        private List<Shirt> GetMatchingShirts(SearchOptions searchOptions)
        {
            var shirts = new List<Shirt>();

            for (var colorIndex = 0; colorIndex < Color.All.Count; colorIndex++)
            {
                for (var sizeIndex = 0; sizeIndex < Size.All.Count; sizeIndex++)
                {
                    if (Color.All[colorIndex].Matches(searchOptions.Colors) && Size.All[sizeIndex].Matches(searchOptions.Sizes))
                    {
                        shirts.AddRange(_shirtsByColorAndSizeCache[colorIndex, sizeIndex]);
                    }
                }
            }

            return shirts;
        }

        private List<ColorCount> GetColorCounts(SearchOptions searchOptions)
        {
            var colorCounts = new List<ColorCount>();

            for (var colorIndex = 0; colorIndex < Color.All.Count; colorIndex++)
            {
                var count = 0;

                for (var sizeIndex = 0; sizeIndex < Size.All.Count; sizeIndex++)
                {
                    if (Size.All[sizeIndex].Matches(searchOptions.Sizes) && Color.All[colorIndex].Matches(searchOptions.Colors))
                    {
                        count += _numberShirtsByColorAndSizeCache[colorIndex, sizeIndex];
                    }
                }

                colorCounts.Add(new ColorCount { Color = Color.All[colorIndex], Count = count });
            }

            return colorCounts;
        }

        private List<SizeCount> GetSizeCounts(SearchOptions searchOptions)
        {
            var sizeCounts = new List<SizeCount>();

            for (var sizeIndex = 0; sizeIndex < Size.All.Count; sizeIndex++)
            {
                var count = 0;

                for (var colorIndex = 0; colorIndex < Color.All.Count; colorIndex++)
                {
                    if (Size.All[sizeIndex].Matches(searchOptions.Sizes) && Color.All[colorIndex].Matches(searchOptions.Colors))
                    {
                        count += _numberShirtsByColorAndSizeCache[colorIndex, sizeIndex];
                    }
                }

                sizeCounts.Add(new SizeCount { Size = Size.All[sizeIndex], Count = count });
            }

            return sizeCounts;
        }
    }
}
