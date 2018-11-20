using System;
using System.Collections.Generic;
using Moq;
using RestSharp;
using Weather.Api.Models;
using Weather.Api.Services;
using Xunit;

namespace Weather.Api.Tests.Service
{
    public class ElevationDataServiceTests : BaseServiceTest<ElevationDataService>
    {
        private readonly ElevationDataService _service;

        public ElevationDataServiceTests()
        {
            _service = new ElevationDataService(_restClientFactory.Object, _memCache.Object, _logger.Object,
                _configBinder.Object);
        }

        [Fact]
        public async void GetResourceAsync_Returns_Valid_Data()
        {
            var data = NewElevationData();

            var response = new Mock<IRestResponse<ElevationData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewElevationData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<ElevationData>(It.IsAny<RestRequest>()))
                .ReturnsAsync(response.Object);

            var result = await _service.GetResourceAsync("-123.434,44.,5");

            Assert.Equal(data.Elevation, result.Elevation);
        }

        [Fact]
        public async void GetResourceAsync_Any_Error_Bubbles_Exception()
        {
            var data = NewElevationData();

            var response = new Mock<IRestResponse<ElevationData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewElevationData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<ElevationData>(It.IsAny<RestRequest>()))
                .Throws(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await _service.GetResourceAsync("-123.434,44.,5"));
        }

        [Fact]
        public async void GetResourceAsync_Caches_Data()
        {
            var data = NewElevationData();

            var response = new Mock<IRestResponse<ElevationData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewElevationData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<ElevationData>(It.IsAny<RestRequest>()))
                .ReturnsAsync(response.Object);

            var result = await _service.GetResourceAsync("-123.434,44.,5");

            Assert.Equal(data.Elevation, result.Elevation);
            _memCache.Verify(m => m.TryGetValue(It.IsAny<string>(), out data), Times.Once);
            _memCache.Verify(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTimeOffset>()),
                Times.Once);
        }
    }
}