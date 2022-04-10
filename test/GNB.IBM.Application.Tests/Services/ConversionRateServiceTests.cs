using AutoMapper;
using GNB.IBM.Application.Mapper;
using GNB.IBM.Application.Models;
using GNB.IBM.Application.Services;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GNB.IBM.Application.Tests.Services
{
    public class ConversionRateServiceTests
    {
        [Fact]
        public async void GetConversionRateList_WhenCalled_ThenCallToRepositoryOnce()
        {
            // Arrange
            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var conversionRateService = new ConversionRateService(fakeIConversionRateRepository.Object, mapper);

            // Act
            IEnumerable<ConversionRateModel> list = await conversionRateService.GetConversionRateListAsync();

            // Assert
            fakeIConversionRateRepository.Verify(x => x.GetConversionRateListAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ConversionRateValues))]
        public async void GetConversionRateList_WhenCalled_ThenReturnsSameNumbersOfItemsReceivedFromRepository
            (List<ConversionRate> conversionRates)
        {
            // Arrange
            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var expected = conversionRates.Count();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(conversionRates);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var conversionRateService = new ConversionRateService(fakeIConversionRateRepository.Object, mapper);

            // Act
            IEnumerable<ConversionRateModel> list = await conversionRateService.GetConversionRateListAsync();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        public static TheoryData<List<ConversionRate>> ConversionRateValues => new()
        {
            {
                new List<ConversionRate>
                {
                     new ConversionRate { Id = 1, From = "A", To = "B", Rate = 1.1f },
                     new ConversionRate { Id = 2, From = "B", To = "A", Rate = 1.2f }
                }
            },
            { new List<ConversionRate>() }
        };
    }
}