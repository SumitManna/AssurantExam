using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Application.Services;
using AssurantTest.Infrastructure.Repository.Coupon;
using AssurantTest.Infrastructure.Repository.Customer;
using AssurantTest.Infrastructure.Repository.Order;
using AssurantTest.Infrastructure.Repository.Product;
using AssurantTest.Infrastructure.Repository.Promotion;
using AssurantTest.Infrastructure.Repository.States;
using AssurantTest.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AssurantTest.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServiceAndConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>()
                .AddScoped<IStateRepository, StateRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICouponRepository, CouponRepository>()
                .AddScoped<IPromotionRepository, PromotionRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ITaxCalculationService, TaxCalculationService>()
                .AddScoped<ICouponService, CouponService>()
                .AddScoped<IPromotionService, PromotionService>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IStateService, StateService>()
                .AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
