using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Application.Interfaces.Services;
using System.Net;

namespace AssurantTest.Infrastructure.Services
{
    public class CouponService(ICouponRepository couponRepository) : ICouponService
    {
        public Task<IReadOnlyCollection<Coupon>> GetCouponsListAsync()
        {
            var data = couponRepository.GetAllCoupons() ?? throw new ApiException((int)HttpStatusCode.NotFound, "Coupons not found");
            return Task.FromResult(data);
        }
    }
}
