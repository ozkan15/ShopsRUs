using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopsRUs.Controllers;
using ShopsRUs.DTO;
using ShopsRUs.Infrastructure;
using ShopsRUs.Services;
using ShopsRUs.ViewModel;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShopsRUs.UnitTests
{
    [TestClass]
    public class DiscountControllerTest
    {
        private readonly ServiceProvider _serviceProvider;
        public DiscountControllerTest()
        {
            var services = new ServiceCollection();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            services.AddDbContext<ShopsRUsContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(connection);
            });
            services.AddTransient<IDiscountService, DiscountService>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task Duplicate_Products_Cant_Be_Added()
        {
            // Arrange
            var data = new BillInputModel
            {
                UserId = 2,
                BillDetails = new List<BillDetailInputModel>
                {
                    new BillDetailInputModel {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new BillDetailInputModel {
                        ProductId = 1,
                        Quantity = 1
                    },
                }
            };

            //Act
            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var badRequest = response.Result as BadRequestObjectResult;

            //assert
            Assert.IsNotNull(badRequest);
            Assert.IsTrue(badRequest.Value as string == "duplicate product found");
        }

        [TestMethod]
        public async Task Product_Quantities_Must_Be_Bigger_Than_Zero()
        {
            var data = new BillInputModel
            {
                UserId = 1,
                BillDetails = new List<BillDetailInputModel>
                {
                    new BillDetailInputModel { ProductId = 1, Quantity= 0 },
                    new BillDetailInputModel { ProductId = 4, Quantity = 0 },
                    new BillDetailInputModel { ProductId = 5, Quantity = 0 }
                }
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var badRequest = response.Result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.IsTrue(badRequest.Value as string == "product quantities must be bigger than 0");
        }

        [TestMethod]
        public async Task ProductList_Cant_Be_Empty()
        {
            var data = new BillInputModel
            {
                UserId = 1,
                BillDetails = new List<BillDetailInputModel>()
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var badRequest = response.Result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.IsTrue(badRequest.Value as string == "product list cant be empty");
        }

        [TestMethod]
        public async Task User_Not_Found()
        {
            var data = new BillInputModel
            {
                UserId = 234,
                BillDetails = new List<BillDetailInputModel>()
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var badRequest = response.Result as BadRequestObjectResult;

            Assert.IsNotNull(badRequest);
            Assert.IsTrue(badRequest.Value as string == "user not found");
        }

        [TestMethod]
        public async Task Calculate_Customer_Invoice()
        {
            var data = new BillInputModel
            {
                UserId = 1,
                BillDetails = new List<BillDetailInputModel>
                {
                    new BillDetailInputModel { ProductId = 1, Quantity= 3 }
                }
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var successResult = response.Result as OkObjectResult;

            Assert.IsNotNull(successResult);
            Assert.IsTrue((successResult.Value as BillModel)?.AmountNet == 285m);
        }

        [TestMethod]
        public async Task Calculate_Affiliate_Invoice()
        {
            var data = new BillInputModel
            {
                UserId = 2,
                BillDetails = new List<BillDetailInputModel>
                {
                    new BillDetailInputModel { ProductId = 1, Quantity= 3 }
                }
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var successResult = response.Result as OkObjectResult;

            Assert.IsNotNull(successResult);
            Assert.IsTrue((successResult.Value as BillModel)?.AmountNet == 255);
        }

        [TestMethod]
        public async Task Calculate_Employee_Invoice()
        {
            var data = new BillInputModel
            {
                UserId = 3,
                BillDetails = new List<BillDetailInputModel>
                {
                    new BillDetailInputModel { ProductId = 1, Quantity= 3 }
                }
            };

            var discountController = InitializeController();
            var response = await discountController.Post(data);
            var successResult = response.Result as OkObjectResult;

            Assert.IsNotNull(successResult);
            Assert.IsTrue((successResult.Value as BillModel)?.AmountNet == 195);
        }

        private DiscountController InitializeController()
        {
            var discountService = _serviceProvider.GetRequiredService<IDiscountService>();
            var context = _serviceProvider.GetRequiredService<ShopsRUsContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new DiscountController(context, discountService);
        }
    }
}