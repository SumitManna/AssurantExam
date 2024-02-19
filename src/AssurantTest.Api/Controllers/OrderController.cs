using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Domain.Models.Exception;
using AssurantTest.Domain.Models.RequestModel;
using AssurantTest.Domain.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AssurantTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService, ILogger<OrderController> logger) : Controller
    {
        [HttpPost("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CalculateOrderData([FromBody, Required] List<OrderRequestModel> orderRequestModels, [Required] Guid customerId)
        {
            try
            {
                logger.LogInformation($"CalculateOrderData method initiated for customer id {customerId}");
                if (!ModelState.IsValid)
                {
                    var errorMessage = new ErrorResponse(
                        HttpStatusCode.BadRequest.ToString(),
                        ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage)
                        );
                    return BadRequest(errorMessage);
                }

                var data = await orderService.SaveOrderDataAsync(orderRequestModels, customerId);
                logger.LogInformation($"CalculateOrderData method has been executed for customer id {customerId}");
                return Ok(data);
            }
            catch (ApiException ex)
            {
                logger.LogError($"Exception : {ex.Message}");
                return StatusCode(ex.StateCode, new ErrorResponse(((HttpStatusCode)ex.StateCode).ToString(), [ex.Message.ToString()]));
            }
        }
    }
}
