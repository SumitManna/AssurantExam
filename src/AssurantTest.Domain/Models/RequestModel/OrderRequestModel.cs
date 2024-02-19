using System.ComponentModel.DataAnnotations;

namespace AssurantTest.Domain.Models.RequestModel
{
    public record OrderRequestModel
    (
        [Required(ErrorMessage ="ProductId is required")]
        Guid ProductId,
        [Required(ErrorMessage ="Quantity is required")]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be greater than 0")]
        int Quantity
    );
}
