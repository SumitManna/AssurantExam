using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Interfaces.Repository
{
    public interface ICouponRepository
    {
        Coupon? GetCoupon(Guid productId, DateTime orderDate);
        IReadOnlyCollection<Coupon>? GetAllCoupons();
    }
}
