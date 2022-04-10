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
    public class ProductTransactionsControllerTests
    {
        [Fact]
        public async void Get_WhenCalled_ThenCallToApplicationServiceOnce()
        {
            // Arrange
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>();
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListAsync()).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

            // Assert
            fakeIProductTransactionService.Verify(x => x.GetProductTransactionListAsync(), Times.Once);
        }

        [Fact]
        public async void Get_WhenThereAreProductTransactionsInTheService_ThenReturnsSameNumbersOfItemsReceivedFromService()
        {
            // Arrange
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>
            {
                new ProductTransactionModel { SKU = "A", Amount = 1.1f, Currency = "A" },
                new ProductTransactionModel { SKU = "B", Amount = 1.2f, Currency = "B" }
            };
            var expected = stubProductTransactions.Count();
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListAsync()).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        [Fact]
        public async void Get_WhenThereAreNotProductTransactionsInTheService_ThenReturnsZeroItems()
        {
            // Arrange
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>();
            var expected = stubProductTransactions.Count();
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListAsync()).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

            // Assert
            Assert.Equal(expected, list.Count());
        }

        [Fact]
        public async void GetWithSku_WhenCalled_ThenCallToApplicationServiceOnce()
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>();
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            ProductTransactionWithTotalDto productTransactionWithTotalDto = await productTransactionsController.Get(sku);

            // Assert
            fakeIProductTransactionService.Verify(x => x.GetProductTransactionListBySkuAsync(sku), Times.Once);
        }

        [Fact]
        public async void GetWithSku_WhenThereAreProductTransactionsInTheService_ThenReturnsSameNumbersOfItemsReceivedFromService()
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>
            {
                new ProductTransactionModel { SKU = sku, Currency = "A", Amount = 1.1f },
                new ProductTransactionModel { SKU = sku, Currency = "B", Amount = 1.1f }
            };
            var expected = stubProductTransactions.Count();
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            ProductTransactionWithTotalDto productTransactionWithTotalDto = await productTransactionsController.Get(sku);

            // Assert
            Assert.Equal(expected, productTransactionWithTotalDto.ProductTransactions.Count());
        }

        [Fact]
        public async void GetWithSku_WhenThereAreNotProductTransactionsInTheService_ThenReturnsZeroItems()
        {
            // Arrange
            var sku = "A";
            var fakeIProductTransactionService = new Mock<IProductTransactionService>();
            var stubProductTransactions = new List<ProductTransactionModel>();
            var expected = 0;
            fakeIProductTransactionService.Setup(x => x.GetProductTransactionListBySkuAsync(It.IsAny<string>())).ReturnsAsync(stubProductTransactions);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WebAPIProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();

            var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

            // Act
            ProductTransactionWithTotalDto productTransactionWithTotalDto = await productTransactionsController.Get(sku);

            // Assert
            Assert.Equal(expected, productTransactionWithTotalDto.ProductTransactions.Count());
        }
    }
}