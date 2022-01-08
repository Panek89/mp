using System.Net.Mime;
using AutoMapper;
using Machines.Api.Models.DTO;
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
        private readonly IMapper _mapper;
        
        public ParameterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAllParameters")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parameter))]
        public async Task<IActionResult> GetParameters()
        {
            var parameters = await _unitOfWork.Parameters.GetAllWithRelationshipAsync(r => r.Machines);
            var parameterDTOs = _mapper.Map<IEnumerable<ParameterDTO>>(parameters);

            return Ok(parameterDTOs);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parameter))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetParameterById(string id)
        {
            var parameter = _unitOfWork.Parameters.FindWithRelationship(p => p.Id == Guid.Parse(id), r => r.Machines);
            if (parameter == null)
            {
                return NotFound();
            }
            else
            {
                var parameterDto = _mapper.Map<ParameterDTO>(parameter);
                return Ok(parameterDto);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Parameter))]
        public async Task<IActionResult> PostParameter([FromBody] Parameter parameter)
        {
            await _unitOfWork.Parameters.AddAsync(parameter);
            await _unitOfWork.CompleteAsync();
            
            return CreatedAtAction(nameof(GetParameterById), new { id = parameter.Id }, parameter);
        }

        [HttpPost("CreateParameters")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Parameter>))]
        public async Task<IActionResult> PostParameters([FromBody] IEnumerable<Parameter> parameters)
        {
            await _unitOfWork.Parameters.AddRangeAsync(parameters);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetParameters), parameters);
        }

        [HttpDelete("RemoveParameterById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveParameterById([FromQuery(Name = "id")] string id)
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

        [HttpDelete("RemoveParametersByKey")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveParametersByKey([FromQuery(Name = "key")] string key)
        {
            var parametersToRemove = _unitOfWork.Parameters.Find(x => x.Key == key);
            if (parametersToRemove.Count() > 0)
            {
                _unitOfWork.Parameters.RemoveRange(parametersToRemove);
                await _unitOfWork.CompleteAsync();
                
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}