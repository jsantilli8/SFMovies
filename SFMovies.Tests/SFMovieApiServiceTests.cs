using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SFMovies.Infrastructure.Services;
using SFMovies.Domain.Entities;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class SFMovieApiServiceTests
{
    private SFMovieApiService CreateService(HttpMessageHandler handler, IMemoryCache cache)
    {
        var httpClient = new HttpClient(handler);
        var logger = new Mock<ILogger<SFMovieApiService>>().Object;
        var configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>().Object;
        return new SFMovieApiService(httpClient, cache, configuration, logger);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsFromApiAndCaches()
    {
        var movies = new List<SFMovieApiResponse> {
            new SFMovieApiResponse { title = "Movie 1", release_year = "2000", locations = "SF" },
            new SFMovieApiResponse { title = "Movie 2", release_year = "2001", locations = "SF" }
        };
        var handler = new MockHttpMessageHandler(JsonSerializer.Serialize(movies));
        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = CreateService(handler, cache);

        var result = await service.GetAllAsync(CancellationToken.None);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(cache.TryGetValue("MovieLocations_All", out _));
    }

    [TestMethod]
    public async Task GetAllAsync_FallbackToCacheOnApiError()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var cachedMovies = new List<MovieLocation> {
            new MovieLocation { Id = 1, Title = "Cached Movie", Locations = "SF" }
        };
        cache.Set("MovieLocations_All", cachedMovies);
        var handler = new MockHttpMessageHandler(null, true); // Simula error
        var service = CreateService(handler, cache);

        var result = await service.GetAllAsync(CancellationToken.None);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("Cached Movie", result.First().Title);
    }

    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly bool _throwError;
        public MockHttpMessageHandler(string response, bool throwError = false)
        {
            _response = response;
            _throwError = throwError;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_throwError)
                throw new HttpRequestException("Simulated error");
            var msg = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_response ?? "[]")
            };
            return Task.FromResult(msg);
        }
    }
}
