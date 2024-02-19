using AssurantTest.Application.Entities;

namespace AssurantTest.Application.Model
{
    public record OrderDataModel
    (
        DateTime OrderDate,
        State State,
        IReadOnlyCollection<ProductDataModel> Products
    );

    public record ProductDataModel(Product Product, int Quantity, Coupon? Coupon);
}
