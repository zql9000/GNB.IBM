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
        public async void GetProductTransactionList_WhenCalled_ThenCallToRepositoryOnce()
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
        public async void GetProductTransactionList_WhenCalled_ThenReturnsSameNumbersOfItemsReceivedFromRepository
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