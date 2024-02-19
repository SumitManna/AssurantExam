using AssurantTest.Application.Entities;
using AssurantTest.Application.Exception;
using AssurantTest.Application.Interfaces.Services;
using AssurantTest.Domain.Models.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AssurantTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController(ICouponService couponService) : ControllerBase
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<Coupon>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await couponService.GetCouponsListAsync();
                return Ok(data);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StateCode, new ErrorResponse(((HttpStatusCode)ex.StateCode).ToString(), [ex.Message.ToString()]));
            }
        }
    }
}
