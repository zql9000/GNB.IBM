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
                fakeIProductTransactionService.Setup(x => x.GetProductTransactionList()).ReturnsAsync(stubProductTransactions);

                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new WebAPIProfile());
                });
                var mapper = mapperConfiguration.CreateMapper();

                var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

                // Act
                IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

                // Assert
                fakeIProductTransactionService.Verify(x => x.GetProductTransactionList(), Times.Once);
            }

            [Fact]
            public async void Get_WhenThereAreProductTransactionsInTheService_ThenReturnsSameNumbersOfItemsReceivedFromService()
            {
                // Arrange
                var fakeIProductTransactionService = new Mock<IProductTransactionService>();
                var productTransactions = new List<ProductTransactionModel>
            {
                    new ProductTransactionModel { SKU = "A", Amount = 1.1f, Currency = "A" },
                    new ProductTransactionModel { SKU = "B", Amount = 1.2f, Currency = "B" }
            };
                var initialConvertionRatesCount = productTransactions.Count();
                fakeIProductTransactionService.Setup(x => x.GetProductTransactionList()).ReturnsAsync(productTransactions);

                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new WebAPIProfile());
                });
                var mapper = mapperConfiguration.CreateMapper();

                var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

                // Act
                IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

                // Assert
                Assert.Equal(initialConvertionRatesCount, list.Count());
            }

            [Fact]
            public async void Get_WhenThereAreNotProductTransactionsInTheService_ThenReturnsZeroItems()
            {
                // Arrange
                var fakeIProductTransactionService = new Mock<IProductTransactionService>();
                var productTransactions = new List<ProductTransactionModel>();
                var initialConvertionRatesCount = productTransactions.Count();
                fakeIProductTransactionService.Setup(x => x.GetProductTransactionList()).ReturnsAsync(productTransactions);

                var mapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new WebAPIProfile());
                });
                var mapper = mapperConfiguration.CreateMapper();

                var productTransactionsController = new ProductTransactionsController(fakeIProductTransactionService.Object, mapper);

                // Act
                IEnumerable<ProductTransactionDto> list = await productTransactionsController.Get();

                // Assert
                Assert.Equal(initialConvertionRatesCount, list.Count());
            }
        }
    }