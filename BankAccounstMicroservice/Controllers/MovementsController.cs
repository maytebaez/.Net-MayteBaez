using AutoMapper;
using BankAccounstMicroservice.Models.Dtos;
using BankAccountsMicroservice.Models;
using BankAccountsMicroservice.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountsMicroservice.Controllers
{
    [ApiController]
    [Route("api/movements")]
    public class MovementsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MovementRepository _repository;

        public MovementsController(MovementRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movement>>> GetMovements()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("reporte/{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetMovements(int id,
                                                                [FromQuery] DateTime initialDate,
                                                                [FromQuery] DateTime endDate)
        {
            return Ok(await _repository.GetMovementsByDateAndClient(id, initialDate, endDate));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movement>> GetMovement(int id)
        {
            var movement = await _repository.GetByIdAsync(id);
            if (movement == null) return NotFound();
            return Ok(movement);
        }

        [HttpPost]
        public async Task<ActionResult> PostMovement(Movement movement)
        {
            await _repository.AddAsync(movement);
            return CreatedAtAction(nameof(GetMovement), new { id = movement.Id }, movement);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovement(int id, [FromBody] MovementRequestDto movementRequestDto)
        {
            var movement = await _repository.GetByIdAsync(id);
            _mapper.Map(movementRequestDto, movement);
            await _repository.UpdateAsync(movement);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMovements(int id, [FromBody] JsonPatchDocument<Movement> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var movement = await _repository.GetByIdAsync(id);
            if (movement == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(movement);

            if (!TryValidateModel(movement))
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAsync(movement);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovement(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}