using Machines.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MP.MachinesApi.Models;

namespace MP.MachinesApi.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class ParameterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ParameterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllParameters")]
        [Produces("application/json")]
        public async Task<IActionResult> GetParameters()
        {
            var parameters = await _unitOfWork.Parameters.GetAllAsync();
            return Ok(parameters);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetParameterById(string id)
        {
            var parameter = await _unitOfWork.Parameters.GetByIdAsync(Guid.Parse(id));
            if (parameter == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(parameter);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> PostParameter([FromBody]Parameter parameter)
        {
            await _unitOfWork.Parameters.AddAsync(parameter);
            await _unitOfWork.CompleteAsync();
            
            return Ok(parameter);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> RemoveParameter(string id)
        {
            var parameterToRemove = await _unitOfWork.Parameters.GetByIdAsync(Guid.Parse(id));
            if (parameterToRemove == null)
            {
                return NotFound();
            }
            else 
            {
                _unitOfWork.Parameters.Remove(parameterToRemove);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        public IActionResult RemoveParameters()
        {
            // TO IMPLEMENT
            return Ok();
        }
    }
}