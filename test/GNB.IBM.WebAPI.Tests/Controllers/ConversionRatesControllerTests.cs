using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.Application.Models;
using GNB.IBM.WebAPI.Controllers;
using GNB.IBM.WebAPI.Dto;
using GNB.IBM.WebAPI.Mapper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GNB.IBM.WebAPI.Tests.Controllers
{
    public class ConversionRatesControllerTests
    {
        [Fact]
        public async void Get_WhenCalled_ThenCallToApplicationServiceOnce()
        {
            // Arrange
            var fakeIConversionRateService = new Mock<IConversionRateService>();
            var stubConversionRates = new List<ConversionRateModel>();
            fakeIConversionRateService.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var conversionRatesController = new ConversionRatesController(fakeIConversionRateService.Object, mapper);

            // Act
            IEnumerable<ConversionRateDto> list = await conversionRatesController.Get();

            // Assert
            fakeIConversionRateService.Verify(x => x.GetConversionRateListAsync(), Times.Once);
        }

        [Fact]
        public async void Get_WhenThereAreConversionRatesInTheService_ThenReturnsSameNumbersOfItemsReceivedFromService()
        {
            // Arrange
            var fakeIConversionRateService = new Mock<IConversionRateService>();
            var stubConversionRates = new List<ConversionRateModel>
            {
                    new ConversionRateModel { From = "A", To = "B", Rate = 1.1f },
                    new ConversionRateModel { From = "B", To = "A", Rate = 1.2f }
            };
            var expected = stubConversionRates.Count();
            fakeIConversionRateService.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var conversionRatesController = new ConversionRatesController(fakeIConversionRateService.Object, mapper);

            // Act
            IEnumerable<ConversionRateDto> list = await conversionRatesController.Get();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        [Fact]
        public async void Get_WhenThereAreNotConversionRatesInTheService_ThenReturnsZeroItems()
        {
            // Arrange
            var fakeIConversionRateService = new Mock<IConversionRateService>();
            var stubConversionRates = new List<ConversionRateModel>();
            var expected = stubConversionRates.Count();
            fakeIConversionRateService.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var conversionRatesController = new ConversionRatesController(fakeIConversionRateService.Object, mapper);

            // Act
            IEnumerable<ConversionRateDto> list = await conversionRatesController.Get();

            // Assert
            Assert.Equal(expected, list.Count());
        }
    }
}