using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GNB.IBM.Infrastructure.Tests.Repositories
{
    public class ProductTransactionRepositoryTests
    {
        [Fact]
        public async void GetAllAsync_WhenCalled_ThenConsumeTheEndpointWithTheURIFromTheSettings()
        {
            // Arrange
            var fakeIHttpHandler = new Mock<IHttpHandler<ProductTransaction>>();
            var stubProductTransactions = new List<ProductTransaction>();
            fakeIHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var fakeIOptionsSnapshot = new Mock<IOptionsSnapshot<ExternalServicesSettings>>();
            var stubExternalServicesSettings = new ExternalServicesSettings();
            var productTransactionsURI = "http://fake-url.com";
            stubExternalServicesSettings.ProductTransactionsURI = productTransactionsURI;
            fakeIOptionsSnapshot.Setup(x => x.Value).Returns(stubExternalServicesSettings);

            var productTransactionRepository = new ProductTransactionRepository(fakeIHttpHandler.Object, fakeIOptionsSnapshot.Object);

            // Act
            IEnumerable<ProductTransaction> list = await productTransactionRepository.GetProductTransactionListAsync();

            // Assert
            fakeIHttpHandler.Verify(x => x.GetAsync(productTransactionsURI), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ProductTransactionValues))]
        public async void GetAllAsync_WhenCalled_ThenReturnsSameNumbersOfItemsReceivedFromHttpHandler
            (List<ProductTransaction> productTransactions)
        {
            // Arrange
            var fakeIHttpHandler = new Mock<IHttpHandler<ProductTransaction>>();
            var stubProductTransactions = productTransactions;
            var expected = productTransactions.Count();
            fakeIHttpHandler.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var fakeIOptionsSnapshot = new Mock<IOptionsSnapshot<ExternalServicesSettings>>();
            var stubExternalServicesSettings = new ExternalServicesSettings();
            var productTransactionsURI = "http://fake-url.com";
            stubExternalServicesSettings.ProductTransactionsURI = productTransactionsURI;
            fakeIOptionsSnapshot.Setup(x => x.Value).Returns(stubExternalServicesSettings);

            var productTransactionRepository = new ProductTransactionRepository(fakeIHttpHandler.Object, fakeIOptionsSnapshot.Object);

            // Act
            IEnumerable<ProductTransaction> list = await productTransactionRepository.GetProductTransactionListAsync();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        public static TheoryData<List<ProductTransaction>> ProductTransactionValues => new()
        {
            {
                new List<ProductTransaction>
                {
                     new ProductTransaction { Id = 1, SKU = "A", Amount = 1.1f, Currency = "A" },
                     new ProductTransaction { Id = 2, SKU = "B", Amount = 1.2f, Currency = "B" }
                }
            },
            { new List<ProductTransaction>() }
        };
    }
}