using System;
using System.Collections.Generic;
using Moq;
using RestSharp;
using Weather.Api.Models;
using Weather.Api.Services;
using Xunit;

namespace Weather.Api.Tests.Service
{
    public class TimeZoneDataServiceTests : BaseServiceTest<TimeZoneDataService>
    {
        private readonly TimeZoneDataService _service;

        public TimeZoneDataServiceTests()
        {
            _service = new TimeZoneDataService(_restClientFactory.Object, _memCache.Object, _logger.Object,
                _configBinder.Object);
        }

        [Fact]
        public async void GetResourceAsync_Returns_Valid_Data()
        {
            var data = NewTimeZoneData();

            var response = new Mock<IRestResponse<TimeZoneData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewTimeZoneData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<TimeZoneData>(It.IsAny<RestRequest>()))
                .ReturnsAsync(response.Object);

            var result = await _service.GetResourceAsync("-123.434,44.,5");

            Assert.Equal(data.TimeZoneName, result.TimeZoneName);
        }

        [Fact]
        public async void GetResourceAsync_Any_Error_Bubbles_Exception()
        {
            var data = NewTimeZoneData();

            var response = new Mock<IRestResponse<TimeZoneData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewTimeZoneData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<TimeZoneData>(It.IsAny<RestRequest>()))
                .Throws(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await _service.GetResourceAsync("-123.434,44.,5"));
        }

        [Fact]
        public async void GetResourceAsync_Caches_Data()
        {
            var data = NewTimeZoneData();

            var response = new Mock<IRestResponse<TimeZoneData>>();
            response.SetupGet(c => c.IsSuccessful).Returns(true);
            response.SetupGet(c => c.Data).Returns(NewTimeZoneData());

            _restClient
                .Setup(c => c.ExecuteGetTaskAsync<TimeZoneData>(It.IsAny<RestRequest>()))
                .ReturnsAsync(response.Object);

            var result = await _service.GetResourceAsync("97219");

            Assert.Equal(data.TimeZoneName, result.TimeZoneName);
            _memCache.Verify(m => m.TryGetValue(It.IsAny<string>(), out data), Times.Once);
            _memCache.Verify(m => m.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTimeOffset>()),
                Times.Once);
        }
    }
}