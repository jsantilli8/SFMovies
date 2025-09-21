using Microsoft.VisualStudio.TestTools.UnitTesting;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace SFMovies.Tests
{
    [TestClass]
    public sealed class MovieLocationCacheServiceTests
    {
        private MovieLocationCacheService CreateServiceWithLocations(List<MovieLocation> locations)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var service = new MovieLocationCacheService(memoryCache);
            service.SetLocationsForTest(locations); // Método especial para test
            return service;
        }

        [TestMethod]
        public void GetAllLocationsAsync_ReturnsAllLocations()
        {
            var locations = new List<MovieLocation>
            {
                new MovieLocation { Id = 1, Title = "Movie 1", Locations = "SF", Latitude = 37.77, Longitude = -122.42 },
                new MovieLocation { Id = 2, Title = "Movie 2", Locations = "SF", Latitude = 37.78, Longitude = -122.43 }
            };
            var service = CreateServiceWithLocations(locations);
            var result = service.GetAllLocationsAsync().Result;
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void SearchLocationsAsync_ReturnsFilteredLocations()
        {
            var locations = new List<MovieLocation>
            {
                new MovieLocation { Id = 1, Title = "Matrix", Locations = "SF" },
                new MovieLocation { Id = 2, Title = "Star Wars", Locations = "SF" }
            };
            var service = CreateServiceWithLocations(locations);
            var result = service.SearchLocationsAsync("Matrix").Result;
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Matrix", result.First().Title);
        }

        [TestMethod]
        public void GetLocationByIdAsync_ReturnsCorrectLocation()
        {
            var locations = new List<MovieLocation>
            {
                new MovieLocation { Id = 1, Title = "Matrix", Locations = "SF" },
                new MovieLocation { Id = 2, Title = "Star Wars", Locations = "SF" }
            };
            var service = CreateServiceWithLocations(locations);
            var result = service.GetLocationByIdAsync(2).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual("Star Wars", result.Title);
        }
    }
}
