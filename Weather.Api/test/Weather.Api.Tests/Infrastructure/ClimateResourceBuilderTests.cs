using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using RestSharp;
using Weather.Api.Models;
using Weather.Api.Services;
using Weather.Api.Tests.Service;
using Xunit;

namespace Weather.Api.Tests.Infrastructure
{
    public class ClimateResourceBuilderTests
    {
        private Mock<IResourceRetrievalService<CityAndWeatherData>> _cityweatherServiceMock;
        private Mock<IResourceRetrievalService<TimeZoneData>> _timeZoneServiceMock;
        private Mock<IResourceRetrievalService<ElevationData>> _elevationServiceMock;
        private Mock<ILogger<ClimateResourceBuilder>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        private readonly ClimateResourceBuilder _builder;

        public ClimateResourceBuilderTests()
        {
            _cityweatherServiceMock = new Mock<IResourceRetrievalService<CityAndWeatherData>>();
            _timeZoneServiceMock = new Mock<IResourceRetrievalService<TimeZoneData>>();
            _elevationServiceMock = new Mock<IResourceRetrievalService<ElevationData>>();
            _loggerMock = new Mock<ILogger<ClimateResourceBuilder>>();

            var climate = BaseServiceTest<CityAndWeatherData>.NewClimate();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(m => m.Map(It.IsAny<CityAndWeatherData>(), It.IsAny<Climate>())).Returns(climate);
            _mapperMock.Setup(m => m.Map(It.IsAny<ElevationData>(), It.IsAny<Climate>())).Returns(climate);
            _mapperMock.Setup(m => m.Map(It.IsAny<TimeZoneData>(), It.IsAny<Climate>())).Returns(climate);
            
            _builder = new ClimateResourceBuilder(_cityweatherServiceMock.Object, _timeZoneServiceMock.Object, _elevationServiceMock.Object,
                _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void BuildCityWeatherData_Assumes_Valid_Input()
        {
            var data = BaseServiceTest<CityAndWeatherData>.NewCityAndWeatherData();

            _cityweatherServiceMock.Setup(c => c.GetResourceAsync(It.IsAny<string>())).Returns(Task.FromResult(data));

            var isCityWeatherBuildValid = await _builder.BuildCityWeatherDataAsync("97219");

            Assert.True(isCityWeatherBuildValid);
        }

        [Fact]
        public async void BuildCityWeatherData_Any_Error_Returns_False()
        {
            var data = BaseServiceTest<CityAndWeatherData>.NewCityAndWeatherData();

            _cityweatherServiceMock
                .Setup(c => c.GetResourceAsync(It.IsAny<string>()))
                .ThrowsAsync(new RetrievalRequestException<CityAndWeatherData>(new Mock<IRestResponse<CityAndWeatherData>>().Object, "error" ));

            var isCityWeatherBuildValid = await _builder.BuildCityWeatherDataAsync("972kl;jkl;19");
            
            Assert.False(isCityWeatherBuildValid);
        }

        [Fact]
        public async void BuildElevation_Assumes_Valid_Input()
        {
            var data = BaseServiceTest<ElevationData>.NewElevationData();

            _elevationServiceMock.Setup(c => c.GetResourceAsync(It.IsAny<string>())).Returns(Task.FromResult(data));

            var isElevationBuildValid = await _builder.BuildElevationAsync("-124.43", "43.3");

            Assert.True(isElevationBuildValid);
        }

        [Fact]
        public async void BuildElevation_Any_Error_Returns_False()
        {
            var data = BaseServiceTest<ElevationData>.NewElevationData();

            _elevationServiceMock
                .Setup(c => c.GetResourceAsync(It.IsAny<string>()))
                .ThrowsAsync(new RetrievalRequestException<ElevationData>(new Mock<IRestResponse<ElevationData>>().Object, "error"));

            var isElevationBuildValid = await _builder.BuildElevationAsync("-124.gsdfg43", "43gsdfg.3");

            Assert.False(isElevationBuildValid);
        }

        [Fact]
        public async void BuildTimeZone_Assumes_Valid_Input()
        {
            var data = BaseServiceTest<TimeZoneData>.NewTimeZoneData();

            _timeZoneServiceMock.Setup(c => c.GetResourceAsync(It.IsAny<string>())).Returns(Task.FromResult(data));

            var isTimeZoneBuildValid = await _builder.BuildTimeZoneAsync("-124.43", "43.3");

            Assert.True(isTimeZoneBuildValid);
        }

        [Fact]
        public async void BuildTimeZone_Any_Error_Returns_False()
        {
            var data = BaseServiceTest<TimeZoneData>.NewTimeZoneData();

            _timeZoneServiceMock
                .Setup(c => c.GetResourceAsync(It.IsAny<string>()))
                .ThrowsAsync(new RetrievalRequestException<TimeZoneData>(new Mock<IRestResponse<TimeZoneData>>().Object, "error"));

            var isTimeZoneBuildValid = await _builder.BuildTimeZoneAsync("-124.gsdfg43", "43gsdfg.3");

            Assert.False(isTimeZoneBuildValid);
        }
    }
}
