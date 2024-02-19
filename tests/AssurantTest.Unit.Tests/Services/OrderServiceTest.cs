using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Application.Model;
using AssurantTest.Domain.Models.RequestModel;
using AssurantTest.Domain.Models.ResponseModel;
using AssurantTest.Infrastructure.Services;
using AssurantTest.Unit.Tests.BaseSetup;
using Moq;
using System.Net;

namespace AssurantTest.Unit.Tests.Services
{
    public class OrderServiceTest : UnitTestBaseSetup
    {
        private readonly Mock<IStateRepository> stateRepository;
        private readonly Mock<ICustomerRepository> customerRepository;
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IPromotionRepository> promotionRepository;
        private readonly Mock<ICouponRepository> couponRepository;
        private readonly Mock<IOrderRepository> orderRepository;
        private readonly Mock<ITaxCalculationService> taxCalculationService;
        private readonly OrderService orderService;

        public OrderServiceTest()
        {
            stateRepository = new Mock<IStateRepository>();
            customerRepository = new Mock<ICustomerRepository>();
            productRepository = new Mock<IProductRepository>();
            promotionRepository = new Mock<IPromotionRepository>();
            couponRepository = new Mock<ICouponRepository>();
            taxCalculationService = new Mock<ITaxCalculationService>();
            orderRepository = new Mock<IOrderRepository>();
            orderService = new OrderService(
                stateRepository.Object,
                productRepository.Object,
                couponRepository.Object,
                promotionRepository.Object,
                orderRepository.Object,
                customerRepository.Object,
                taxCalculationService.Object
                );
        }

        [Test]
        public async Task When_Execute_CalculateTotalCost()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "NY", "NY1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var coupon1 = new Coupon(Guid.NewGuid(), prod1.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.05m);
            var coupon2 = new Coupon(Guid.NewGuid(), prod2.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.10m);

            var productData1 = new ProductDataModel(prod1, 1, coupon1);
            var productData2 = new ProductDataModel(prod2, 1, coupon2);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            decimal preTaxCost = 20.0m, taxAmount = 1.0m, discountAmout = 2.0m;

            taxCalculationService.Setup(x => x.CalculateDiscountAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(discountAmout);
            taxCalculationService.Setup(x => x.CalculatePreTaxCostAsync(It.IsAny<OrderDataModel>())).ReturnsAsync(preTaxCost);
            taxCalculationService.Setup(x => x.CalculateTaxAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(taxAmount);

            //Act
            var result = await orderService.CalculateTotalCostAsync(orderData, promotion, Guid.NewGuid());

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalCost, Is.EqualTo(19.00));
        }

        [Test]
        public async Task When_Execute_SaveOrderData()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "NY", "NY1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var customerDetail = new Customer(Guid.NewGuid(), state.Id, "Test Customer");

            var coupon1 = new Coupon(Guid.NewGuid(), prod1.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.05m);
            var coupon2 = new Coupon(Guid.NewGuid(), prod2.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.10m);

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var productData1 = new ProductDataModel(prod1, 1, coupon1);
            var productData2 = new ProductDataModel(prod2, 1, coupon2);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            var order1 = new OrderRequestModel(prod1.Id, 2);
            var order2 = new OrderRequestModel(prod2.Id, 1);

            var listOrder = new List<OrderRequestModel> { order1, order2 };

            decimal preTaxCost = 20.0m, taxAmount = 1.0m, discountAmout = 2.0m;

            taxCalculationService.Setup(x => x.CalculateDiscountAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(discountAmout);
            taxCalculationService.Setup(x => x.CalculatePreTaxCostAsync(It.IsAny<OrderDataModel>())).ReturnsAsync(preTaxCost);
            taxCalculationService.Setup(x => x.CalculateTaxAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(taxAmount);

            customerRepository.Setup(x => x.GetCustomer(It.IsAny<Guid>())).Returns(customerDetail);
            stateRepository.Setup(x => x.GetStateData(It.IsAny<Guid>())).Returns(state);
            productRepository.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(prod1);
            couponRepository.Setup(x => x.GetCoupon(It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(coupon1);
            promotionRepository.Setup(x => x.GetPromotionData(It.IsAny<DateTime>())).Returns(promotion);
            orderRepository.Setup(x => x.SaveOrderData(It.IsAny<OrderResponseModel>())).Returns(true);

            //Act
            var result = await orderService.SaveOrderDataAsync(listOrder, customerDetail.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task When_Execute_SaveOrderData_UnableToSaveData()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "NY", "NY1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var customerDetail = new Customer(Guid.NewGuid(), state.Id, "Test Customer");

            var coupon1 = new Coupon(Guid.NewGuid(), prod1.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.05m);
            var coupon2 = new Coupon(Guid.NewGuid(), prod2.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.10m);

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var productData1 = new ProductDataModel(prod1, 1, coupon1);
            var productData2 = new ProductDataModel(prod2, 1, coupon2);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            var order1 = new OrderRequestModel(prod1.Id, 2);
            var order2 = new OrderRequestModel(prod2.Id, 1);

            var listOrder = new List<OrderRequestModel> { order1, order2 };

            decimal preTaxCost = 20.0m, taxAmount = 1.0m, discountAmout = 2.0m;

            taxCalculationService.Setup(x => x.CalculateDiscountAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(discountAmout);
            taxCalculationService.Setup(x => x.CalculatePreTaxCostAsync(It.IsAny<OrderDataModel>())).ReturnsAsync(preTaxCost);
            taxCalculationService.Setup(x => x.CalculateTaxAmountAsync(It.IsAny<OrderDataModel>(), It.IsAny<Promotion>())).ReturnsAsync(taxAmount);

            customerRepository.Setup(x => x.GetCustomer(It.IsAny<Guid>())).Returns(customerDetail);
            stateRepository.Setup(x => x.GetStateData(It.IsAny<Guid>())).Returns(state);
            productRepository.Setup(x => x.GetProductById(It.IsAny<Guid>())).Returns(prod1);
            couponRepository.Setup(x => x.GetCoupon(It.IsAny<Guid>(), It.IsAny<DateTime>())).Returns(coupon1);
            promotionRepository.Setup(x => x.GetPromotionData(It.IsAny<DateTime>())).Returns(promotion);
            orderRepository.Setup(x => x.SaveOrderData(It.IsAny<OrderResponseModel>())).Returns(false);

            //Act
            var ex = Assert.ThrowsAsync<ApiException>(async () => await orderService.SaveOrderDataAsync(listOrder, customerDetail.Id));

            //Assert
            Assert.That(ex.StateCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }
    }
}
