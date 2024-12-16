using AutoMapper;
using BankClientsMicroservice.Models;
using BankClientstMicroservice.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankClientsMicroservice.Controllers
{
[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
        private readonly IMapper _mapper;

        private readonly IRepository<Client> _repository;

        public ClientsController(IRepository<Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientResponseDto>>> GetClients()
        {
            var clients = await _repository.GetAllAsync();
            var clientsDto = _mapper.Map<IEnumerable<ClientResponseDto>>(clients);
            return Ok(clientsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponseDto>> GetClient(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return NotFound();
            var clientDto = _mapper.Map<ClientResponseDto>(client);
            return Ok(clientDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostClient([FromBody] Client client)
        {
            await _repository.AddAsync(client);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, [FromBody] ClientRequestDto clientRequestDto)
        {
            var client = await _repository.GetByIdAsync(id);
            _mapper.Map(clientRequestDto, client);
            await _repository.UpdateAsync(client);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchClients(int id, [FromBody] JsonPatchDocument<Client> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var client = await _repository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(client);

            if (!TryValidateModel(client))
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAsync(client);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}