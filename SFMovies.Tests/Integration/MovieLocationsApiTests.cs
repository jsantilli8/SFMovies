using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SFMovies.Api;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;
using System.Net.Http.Json;

namespace SFMovies.Tests.Integration
{
    [TestClass]
    public class MovieLocationsApiTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private static readonly MovieLocation TestMovie = new()
        {
            Id = 1,
            Title = "Test Movie",
            Locations = "SF"
        };

        [TestInitialize]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddScoped<IMovieLocationService>(sp =>
                        {
                            var service = sp.GetRequiredService<MovieLocationCacheService>();
                            service.SetLocationsForTest(new List<MovieLocation> { TestMovie });
                            return service;
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task GetAllLocations_ReturnsSuccessAndData()
        {
            // Act
            var response = await _client.GetAsync("/api/movielocations");
            response.EnsureSuccessStatusCode();
            var locations = await response.Content.ReadFromJsonAsync<IEnumerable<MovieLocation>>();

            // Assert
            Assert.IsNotNull(locations);
            Assert.AreEqual(1, locations.Count());
            Assert.AreEqual(TestMovie.Title, locations.First().Title);
        }

        [TestMethod]
        public async Task SearchLocations_WithValidTitle_ReturnsFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/movielocations/search?title=Test");
            response.EnsureSuccessStatusCode();
            var locations = await response.Content.ReadFromJsonAsync<IEnumerable<MovieLocation>>();

            // Assert
            Assert.IsNotNull(locations);
            Assert.AreEqual(1, locations.Count());
            Assert.AreEqual(TestMovie.Title, locations.First().Title);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}