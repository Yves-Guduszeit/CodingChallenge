using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Extensions;
using ConstructionLine.CodingChallenge.Models;

namespace ConstructionLine.CodingChallenge.Caches
{
    public class ShirtsByColorAndSizeCache
    {
        private readonly List<Shirt>[,] _shirtsByColorAndSize;
        
        public ShirtsByColorAndSizeCache(int numberColors, int numberSizes)
        {
            _shirtsByColorAndSize = new List<Shirt>[numberColors, numberSizes];

            for (var colorIndex = 0; colorIndex < numberColors; colorIndex++)
            {
                for (var sizeIndex = 0; sizeIndex < numberSizes; sizeIndex++)
                {
                    _shirtsByColorAndSize[colorIndex, sizeIndex] = new List<Shirt>();
                }
            }

            NumberColors = numberColors;
            NumberSizes = numberSizes;
        }

        public void Init(IEnumerable<Shirt> shirts)
        {
            foreach (var shirt in shirts)
            {
                var colorIndex = shirt.Color.Index();
                var sizeIndex = shirt.Size.Index();
                var matchingShirts = _shirtsByColorAndSize[colorIndex, sizeIndex];
                matchingShirts.Add(shirt);
            }
        }

        public List<Shirt> this[int colorIndex, int sizeIndex]
        {
            get => _shirtsByColorAndSize[colorIndex, sizeIndex];
            set => _shirtsByColorAndSize[colorIndex, sizeIndex] = value;
        }

        public int NumberColors { get; }

        public int NumberSizes { get; }
    }
}
