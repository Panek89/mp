using System.Net.Mime;
using Machines.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MP.MachinesApi.Models;

namespace MP.MachinesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MachineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllMachines")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        public async Task<IActionResult> GetMachines()
        {
            var machines = await _unitOfWork.Machines.GetAllAsync();
            return Ok(machines);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMachineById(string id)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(Guid.Parse(id));
            if (machine == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(machine);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        public async Task<IActionResult> PostParameter([FromBody] Machine machine)
        {
            await _unitOfWork.Machines.AddAsync(machine);
            await _unitOfWork.CompleteAsync();
            
            return CreatedAtAction(nameof(GetMachineById), new { id = machine.Id }, machine);
        }

        [HttpPost("CreateMachines")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Machine>))]
        public async Task<IActionResult> PostParameters([FromBody] IEnumerable<Machine> machines)
        {
            await _unitOfWork.Machines.AddRangeAsync(machines);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetMachines), machines);
        }

        [HttpDelete("RemoveMachineById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveMachineById([FromQuery(Name = "id")] string id)
        {
            var machineToRemove = await _unitOfWork.Machines.GetByIdAsync(Guid.Parse(id));
            if (machineToRemove == null)
            {
                return NotFound();
            }
            else 
            {
                _unitOfWork.Machines.Remove(machineToRemove);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
        }

        [HttpDelete("RemoveMachineByManufacturer")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveMachineByManufacturer([FromQuery(Name = "manufacturer")] string manufacturer)
        {
            var machinesToRemove = _unitOfWork.Machines.Find(x => x.Manufacturer == manufacturer);
            if (machinesToRemove == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Machines.RemoveRange(machinesToRemove);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
        }
    }
}