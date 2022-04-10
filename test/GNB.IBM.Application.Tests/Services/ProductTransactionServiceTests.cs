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
    public class ProductTransactionServiceTests
    {
        [Fact]
        public async void GetProductTransactionListAsync_WhenCalled_ThenCallToRepositoryOnce()
        {
            // Arrange
            var fakeIProductTransactionRepository = new Mock<IProductTransactionRepository>();
            var stubProductTransactions = new List<ProductTransaction>();
            fakeIProductTransactionRepository.Setup(x => x.GetProductTransactionListAsync()).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var productTransactionService = new ProductTransactionService(fakeIProductTransactionRepository.Object, mapper, fakeIConversionRateRepository.Object);

            // Act
            IEnumerable<ProductTransactionModel> list = await productTransactionService.GetProductTransactionListAsync();

            // Assert
            fakeIProductTransactionRepository.Verify(x => x.GetProductTransactionListAsync(), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ProductTransactionValues))]
        public async void GetProductTransactionListAsync_WhenCalled_ThenReturnsSameNumbersOfItemsReceivedFromRepository
            (List<ProductTransaction> productTransactions)
        {
            // Arrange
            var fakeIProductTransactionRepository = new Mock<IProductTransactionRepository>();
            var expected = productTransactions.Count();
            fakeIProductTransactionRepository.Setup(x => x.GetProductTransactionListAsync()).ReturnsAsync(productTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var productTransactionService = new ProductTransactionService(fakeIProductTransactionRepository.Object, mapper, fakeIConversionRateRepository.Object);

            // Act
            IEnumerable<ProductTransactionModel> list = await productTransactionService.GetProductTransactionListAsync();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        [Fact]
        public async void GetProductTransactionListBySkuAsync_WhenCalled_ThenCallToRepositoryOnce()
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionRepository = new Mock<IProductTransactionRepository>();
            var stubProductTransactions = new List<ProductTransaction>();
            fakeIProductTransactionRepository.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var productTransactionService = new ProductTransactionService(fakeIProductTransactionRepository.Object, mapper, fakeIConversionRateRepository.Object);

            // Act
            IEnumerable<ProductTransactionModel> list = await productTransactionService.GetProductTransactionListBySkuAsync(sku);

            // Assert
            fakeIProductTransactionRepository.Verify(x => x.GetProductTransactionListBySkuAsync(sku), Times.Once);
        }

        [Theory]
        [MemberData(nameof(ProductTransactionValues))]
        public async void GetProductTransactionListBySkuAsync_WhenCalledWithoutValidConversionRates_ThenReturnsSameNumbersOfItemsReceivedFromRepository
            (List<ProductTransaction> productTransactions)
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionRepository = new Mock<IProductTransactionRepository>();
            var expected = productTransactions.Count();
            fakeIProductTransactionRepository.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(productTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>();
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var productTransactionService = new ProductTransactionService(fakeIProductTransactionRepository.Object, mapper, fakeIConversionRateRepository.Object);

            // Act
            IEnumerable<ProductTransactionModel> list = await productTransactionService.GetProductTransactionListBySkuAsync(sku);

            // Assert
            Assert.Equal(expected, list.Count());
        }

        [Theory]
        [MemberData(nameof(ProductTransactionValues))]
        public async void GetProductTransactionListBySkuAsync_WhenCalledWithValidConversionRates_ThenReturnsSameNumbersOfItemsReceivedFromRepository
            (List<ProductTransaction> productTransactions)
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionRepository = new Mock<IProductTransactionRepository>();
            var expected = productTransactions.Count();
            fakeIProductTransactionRepository.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(productTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var fakeIConversionRateRepository = new Mock<IConversionRateRepository>();
            var stubConversionRates = new List<ConversionRate>
            {
                new ConversionRate { From = "A", To = "EUR", Rate = 0.8f },
                new ConversionRate { From = "B", To = "EUR", Rate = 0.1f }
            };
            fakeIConversionRateRepository.Setup(x => x.GetConversionRateListAsync()).ReturnsAsync(stubConversionRates);

            var productTransactionService = new ProductTransactionService(fakeIProductTransactionRepository.Object, mapper, fakeIConversionRateRepository.Object);

            // Act
            IEnumerable<ProductTransactionModel> list = await productTransactionService.GetProductTransactionListBySkuAsync(sku);

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