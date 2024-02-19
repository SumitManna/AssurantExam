namespace AssurantTest.Domain.Models.ResponseModel
{
    public record OrderResponseModel
    (
        Guid OrderId,
        DateTime OrderDate,
        Guid CustomerId,
        decimal TotalCost,
        decimal PreTaxCost,
        decimal TaxAmount,
        decimal DiscountAmount
    );
}
