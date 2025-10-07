using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SFMovies.Api;
using SFMovies.Application.DTOs;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Linq;

namespace SFMovies.Tests.Integration
{
    [TestClass]
    public class MovieLocationsApiTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [TestInitialize]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task GetAllLocations_ReturnsSuccessAndData()
        {
            // Act
            var response = await _client.GetAsync("/api/movielocations");
            response.EnsureSuccessStatusCode();
            var locations = await response.Content.ReadFromJsonAsync<IEnumerable<MovieLocationDto>>();

            // Assert
            Assert.IsNotNull(locations);
            Assert.IsTrue(locations.Any());
        }

        [TestMethod]
        public async Task SearchLocations_WithValidTitle_ReturnsFilteredResults()
        {
            // Act
            var response = await _client.GetAsync("/api/movielocations/search?title=San Francisco");
            response.EnsureSuccessStatusCode();
            var locations = await response.Content.ReadFromJsonAsync<IEnumerable<MovieLocationDto>>();

            // Assert
            Assert.IsNotNull(locations);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}