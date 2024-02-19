using AssurantTest.Application.Interfaces.Repository;
using AssurantTest.Infrastructure.Common.Utility;
using System.Text.Json;

namespace AssurantTest.Infrastructure.Repository.Coupon
{
    public class CouponRepository : ICouponRepository
    {
        public IReadOnlyCollection<Application.Entities.Coupon>? GetAllCoupons()
        {
            var couponListString = SeedData.ReadSeedData("Coupons.json");
            if (string.IsNullOrEmpty(couponListString))
            {
                return null;
            }

            return JsonSerializer.Deserialize<IReadOnlyCollection<Application.Entities.Coupon>>(couponListString);
        }

        public Application.Entities.Coupon? GetCoupon(Guid productId, DateTime orderDate)
        {
            var couponList = GetAllCoupons();

            if (couponList == null) return null;

            return couponList.FirstOrDefault(c => c.ProductId == productId && c.ValidFrom <= orderDate && c.ValidTo >= orderDate);
        }
    }
}
