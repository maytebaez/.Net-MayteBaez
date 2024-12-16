using BankClientsMicroservice.Models;
using BankClientsMicroservice.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankClientsMicroservice.Controllers 
{
[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
        private readonly AccountsService _service;

        public ReportsController(AccountsService service)
        {
            _service = service;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<object>> GetMovements(int clientId, 
                                                                [FromQuery] DateTime initialDate, 
                                                                [FromQuery] DateTime endDate)
        {
            return Ok(await _service.GetAccountMovemenets(clientId, initialDate, endDate));
        }
    }
}