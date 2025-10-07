using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SFMovies.Application.Handlers;
using SFMovies.Application.Queries;
using SFMovies.Application.DTOs;
using SFMovies.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFMovies.Tests.Handlers
{
    [TestClass]
    public class QueryHandlersTests
    {
        [TestMethod]
        public async Task GetAllMovieLocationsHandler_ReturnsAllLocations()
        {
            var mockService = new Mock<IMovieLocationService>();
            var locations = new List<MovieLocationDto>
            {
                new MovieLocationDto { Id = 1, Title = "Movie 1" },
                new MovieLocationDto { Id = 2, Title = "Movie 2" }
            };
            mockService.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(locations);
            var handler = new GetAllMovieLocationsHandler(mockService.Object);
            var result = await handler.Handle(new GetAllMovieLocationsQuery(), CancellationToken.None);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task SearchMovieLocationsByTitleHandler_ReturnsFilteredLocations()
        {
            var mockService = new Mock<IMovieLocationService>();
            var locations = new List<MovieLocationDto>
            {
                new MovieLocationDto { Id = 1, Title = "Matrix" }
            };
            mockService.Setup(x => x.SearchByTitleAsync("Matrix", It.IsAny<CancellationToken>())).ReturnsAsync(locations);
            var handler = new SearchMovieLocationsByTitleHandler(mockService.Object);
            var result = await handler.Handle(new SearchMovieLocationsByTitleQuery("Matrix"), CancellationToken.None);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Matrix", result.First().Title);
        }

        [TestMethod]
        public async Task GetMovieLocationByIdHandler_ReturnsNull_WhenNotFound()
        {
            var mockService = new Mock<IMovieLocationService>();
            mockService.Setup(x => x.GetByIdAsync(99, It.IsAny<CancellationToken>())).ReturnsAsync((MovieLocationDto?)null);
            var handler = new GetMovieLocationByIdHandler(mockService.Object);
            var result = await handler.Handle(new GetMovieLocationByIdQuery(99), CancellationToken.None);
            Assert.IsNull(result);
        }
    }
}