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
    public class StateController(IStateService stateService) : Controller
    {
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<State>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await stateService.GetStateListAsync();
                return Ok(data);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StateCode, new ErrorResponse(((HttpStatusCode)ex.StateCode).ToString(), [ex.Message.ToString()]));
            }
        }
    }
}
