using AutoMapper;
using BankAccounstMicroservice.Models.Dtos;
using BankAccountsMicroservice.Models;
using BankAccountsMicroservice.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountsMicroservice.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Account> _repository;

        public AccountsController(IRepository<Account> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult> PostAccount(Account account)
        {
            await _repository.AddAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Number }, account);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAccount(string id, [FromBody] AccountRequestDto accountRequestDto)
        {
            var account = await _repository.GetByIdAsync(id);
            _mapper.Map(accountRequestDto, account);
            await _repository.UpdateAsync(account);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAccounts(int id, [FromBody] JsonPatchDocument<Account> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var account = await _repository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(account);

            if (!TryValidateModel(account))
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAsync(account);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}