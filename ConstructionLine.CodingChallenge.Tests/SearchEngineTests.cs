using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Engine;
using ConstructionLine.CodingChallenge.Models;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        #region Initial Test

        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        #endregion

        #region Shirts Results Tests

        [Test]
        public void Search_With_Empty_Color_And_Size_Options_Should_Return_All_Shirts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void Search_With_All_Colors_And_Sizes_Options_Should_Return_All_Shirts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Yellow, Color.White, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void Search_With_Some_Colors_Options_Should_Return_Only_Shirts_Of_These_Colors()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Yellow }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void Search_With_Some_Sizes_Options_Should_Return_Only_Shirts_Of_These_Sizes()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }


        [Test]
        public void Search_With_Some_Colors_And_Some_Size_Options_Should_Return_Only_Shirts_Of_Both_These_Colors_And_Sizes()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        #endregion

        #region Color Counts Results Tests

        [Test]
        public void Search_With_Empty_Color_And_Size_Options_Should_Return_All_Color_Counts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_With_All_Colors_And_Sizes_Options_Should_Return_All_Color_Counts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Yellow, Color.White, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_Should_Return_Only_Color_Counts_Matching_To_Given_Sizes_But_Not_Depending_To_Given_Colors()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        #endregion

        #region Size Counts Results Tests

        [Test]
        public void Search_With_Empty_Color_And_Size_Options_Should_Return_All_Size_Counts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
        }

        [Test]
        public void Search_With_All_Colors_And_Sizes_Options_Should_Return_All_Size_Counts()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Yellow, Color.White, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
        }

        [Test]
        public void Search_Should_Return_Only_Size_Counts_Matching_To_Given_Colors_But_Not_Depending_To_Given_Sizes()
        {
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Yellow },
                Sizes = new List<Size> { Size.Small, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
        }

        #endregion

        #region Test Data

        private static List<Shirt> GetShirts()
        {
            return new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Yellow - Small", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Yellow - Medium", Size.Medium, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Medium", Size.Medium, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Yellow - Large", Size.Large, Color.Yellow),
                new Shirt(Guid.NewGuid(), "White - Large", Size.Large, Color.White),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black)
            };
        }

        #endregion
    }
}
