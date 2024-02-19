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
    public class CustomerController(ICustomerService customerService) : Controller
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<Customer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await customerService.GetCustomerListAsync();
                return Ok(data);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StateCode, new ErrorResponse(((HttpStatusCode)ex.StateCode).ToString(), [ex.Message.ToString()]));
            }
        }
    }
}
