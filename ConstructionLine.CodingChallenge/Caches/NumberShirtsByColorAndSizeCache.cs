using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Caches
{
    public class NumberShirtsByColorAndSizeCache
    {
        private readonly ShirtsByColorAndSizeCache _shirtsByColorAndSizeCache;
        private readonly int[,] _numberShirtsByColorAndSize;

        public NumberShirtsByColorAndSizeCache(ShirtsByColorAndSizeCache shirtsByColorAndSizeCache)
        {
            _shirtsByColorAndSizeCache = shirtsByColorAndSizeCache;

            var numberColors = shirtsByColorAndSizeCache.NumberColors;
            var numberSizes = shirtsByColorAndSizeCache.NumberSizes;
            _numberShirtsByColorAndSize = new int[numberColors, numberSizes];
        }

        public void InitFromCache()
        {
            for (var colorIndex = 0; colorIndex < Color.All.Count; colorIndex++)
            {
                for (var sizeIndex = 0; sizeIndex < Size.All.Count; sizeIndex++)
                {
                    _numberShirtsByColorAndSize[colorIndex, sizeIndex] = _shirtsByColorAndSizeCache[colorIndex, sizeIndex].Count;
                }
            }
        }

        public int this[int colorIndex, int sizeIndex]
        {
            get => _numberShirtsByColorAndSize[colorIndex, sizeIndex];
            set => _numberShirtsByColorAndSize[colorIndex, sizeIndex] = value;
        }
    }
}
