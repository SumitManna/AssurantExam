using AssurantTest.Application.Entities;
using AssurantTest.Application.Model;
using AssurantTest.Application.Services;
using AssurantTest.Unit.Tests.BaseSetup;

namespace AssurantTest.Unit.Tests.Services
{
    public class TaxCalculationServiceTest : UnitTestBaseSetup
    {
        private readonly TaxCalculationService taxCalculationService;
        public TaxCalculationServiceTest()
        {
            taxCalculationService = new TaxCalculationService(configuration);
        }

        [Test]
        public void When_Execute_CalculateCouponDiscount()
        {
            // Arrange
            decimal itemprice = 10;
            int quantity = 1;
            var coupon1 = new Coupon(Guid.NewGuid(), Guid.Empty, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.1m);

            //Act
            var result = taxCalculationService.CalculateCouponDiscount(itemprice, quantity, coupon1);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task When_Execute_CalculatePreTaxCostAsync()
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

            //Act
            var result = await taxCalculationService.CalculatePreTaxCostAsync(orderData);

            //Assert
            Assert.That(result, Is.EqualTo(30));
        }

        [Test]
        public async Task When_Execute_CalculatePromotionDiscountAsync()
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

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculatePromotionDiscountAsync(orderData, promotion);

            //Assert
            Assert.That(result, Is.EqualTo(1.50));
        }

        [Test]
        public async Task When_Execute_CalculateDiscountAmountAsync_WithoutCoupon()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "NY", "NY1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var productData1 = new ProductDataModel(prod1, 1, null);
            var productData2 = new ProductDataModel(prod2, 1, null);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculateDiscountAmountAsync(orderData, promotion);

            //Assert
            Assert.That(result, Is.EqualTo(1.50));
        }

        [Test]
        public async Task When_Execute_CalculateDiscountAmountAsync_WithOutPromotion()
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

            //Act
            var result = await taxCalculationService.CalculateDiscountAmountAsync(orderData, null);

            //Assert
            Assert.That(result, Is.EqualTo(2.50));
        }

        [Test]
        public async Task When_Execute_CalculateDiscountAmountAsync_WithoutCoupon_WithOutPromotion()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "NY", "NY1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var productData1 = new ProductDataModel(prod1, 1, null);
            var productData2 = new ProductDataModel(prod2, 1, null);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculateDiscountAmountAsync(orderData, null);

            //Assert
            Assert.That(result, Is.EqualTo(0.0));
        }

        [Test]
        public async Task When_Execute_CalculateDiscountAmountAsync()
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

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculateDiscountAmountAsync(orderData, promotion);

            //Assert
            Assert.That(result, Is.EqualTo(4.0));
        }

        [Test]
        public async Task When_Execute_CalculateTaxAmountAsync()
        {
            // Arrange
            var state = new State(Guid.NewGuid(), "FL", "FL1", 0.1m, true);
            var prod1 = new Product(Guid.NewGuid(), "Test Product", 10m, true);
            var prod2 = new Product(Guid.NewGuid(), "Test Product2", 20m, false);

            var coupon1 = new Coupon(Guid.NewGuid(), prod1.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.05m);
            var coupon2 = new Coupon(Guid.NewGuid(), prod2.Id, "test", DateTime.Now, DateTime.Now.AddDays(1), 0.10m);

            var productData1 = new ProductDataModel(prod1, 1, coupon1);
            var productData2 = new ProductDataModel(prod2, 1, coupon2);

            var products = new List<ProductDataModel> { productData1, productData2 };

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculateTaxAmountAsync(orderData, promotion);

            //Assert
            Assert.That(result, Is.EqualTo(4.00));
        }

        [Test]
        public async Task When_Execute_CalculateTaxAmountAsync_AfterDiscount()
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

            var promotion = new Promotion(Guid.NewGuid(), 0.05m, DateTime.Now, DateTime.Now.AddDays(1));

            var orderData = new OrderDataModel(DateTime.Now, state, products);

            //Act
            var result = await taxCalculationService.CalculateTaxAmountAsync(orderData, promotion);

            //Assert
            Assert.That(result, Is.EqualTo(2.60));
        }
    }
}
