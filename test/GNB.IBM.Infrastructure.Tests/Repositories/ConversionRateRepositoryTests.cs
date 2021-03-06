using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Infrastructure.Data;
using GNB.IBM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GNB.IBM.Infrastructure.Tests.Repositories
{
    public class ConversionRateRepositoryTests
    {
        [Fact]
        public async void GetAllAsync_WhenCalled_ThenConsumeTheEndpointWithTheURIFromTheSettings()
        {
            // Arrange
            var fakeIHttpHandler = new Mock<IHttpHandler<ConversionRate>>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(stubConversionRates);

            var fakeIOptionsSnapshot = new Mock<IOptionsSnapshot<ExternalServicesSettings>>();
            var stubExternalServicesSettings = new ExternalServicesSettings();
            var conversionRatesURI = "http://fake-url.com";
            stubExternalServicesSettings.ConversionRatesURI = conversionRatesURI;
            fakeIOptionsSnapshot.Setup(x => x.Value).Returns(stubExternalServicesSettings);

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("IBMTest")
                .Options;
            var fakeDatabaseContext = new DatabaseContext(options);

            var conversionRateRepository = new ConversionRateRepository(
                fakeIHttpHandler.Object, 
                fakeIOptionsSnapshot.Object, 
                fakeDatabaseContext);

            // Act
            IEnumerable<ConversionRate> list = await conversionRateRepository.GetConversionRateListAsync();

            // Assert
            fakeIHttpHandler.Verify(x => x.GetAsync(conversionRatesURI), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ConversionRateValues))]
        public async void GetAllAsync_WhenCalled_ThenReturnsSameNumbersOfItemsReceivedFromHttpHandler
            (List<ConversionRate> conversionRates)
        {
            // Arrange
            var fakeIHttpHandler = new Mock<IHttpHandler<ConversionRate>>();
            var stubConversionRates = conversionRates;
            var expected = conversionRates.Count();
            fakeIHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(stubConversionRates);

            var fakeIOptionsSnapshot = new Mock<IOptionsSnapshot<ExternalServicesSettings>>();
            var stubExternalServicesSettings = new ExternalServicesSettings();
            var conversionRatesURI = "http://fake-url.com";
            stubExternalServicesSettings.ConversionRatesURI = conversionRatesURI;
            fakeIOptionsSnapshot.Setup(x => x.Value).Returns(stubExternalServicesSettings);

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("IBMTest")
                .Options;
            var fakeDatabaseContext = new DatabaseContext(options);

            var conversionRateRepository = new ConversionRateRepository(
                fakeIHttpHandler.Object, 
                fakeIOptionsSnapshot.Object,
                fakeDatabaseContext);

            // Act
            IEnumerable<ConversionRate> list = await conversionRateRepository.GetConversionRateListAsync();

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