using System.Net.Mime;
using AutoMapper;
using Machines.Api.Models.DTO;
using Machines.Domain.Enums;
using Machines.Domain.Helpers;
using Machines.Domain.Interfaces;
using Machines.EventServiceBus.Services.Machines;
using Microsoft.AspNetCore.Mvc;
using MP.MachinesApi.Models;

namespace MP.MachinesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMachineSender _machineSender;

        public MachineController(IUnitOfWork unitOfWork, IMapper mapper, IMachineSender machineSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _machineSender = machineSender;
        }

        [HttpGet("GetAllMachines")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        public async Task<IActionResult> GetMachines()
        {
            var machines = await _unitOfWork.Machines.GetAllWithRelationshipAsync(r => r.Parameters);
            var machineDTOs = _mapper.Map<IEnumerable<MachineDTO>>(machines);
            return Ok(machineDTOs);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMachineById(string id)
        {
            var machine = _unitOfWork.Machines.FindWithRelationship(m => m.Id == Guid.Parse(id), r => r.Parameters);
            if (machine == null)
            {
                return NotFound();
            }
            else
            {
                var machineDto = _mapper.Map<MachineDTO>(machine);
                return Ok(machineDto);
            }
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Machine))]
        public async Task<IActionResult> PostMachine([FromBody] Machine machine)
        {
            _machineSender.SendMachine(ServiceBusMachineHelper.MapSingleServiceBusMachineDTO(machine, ServiceBusEnumStatus.Create));
            await _unitOfWork.Machines.AddAsync(machine);
            await _unitOfWork.CompleteAsync();
            
            return CreatedAtAction(nameof(GetMachineById), new { id = machine.Id }, machine);
        }

        [HttpPost("CreateMachines")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Machine>))]
        public async Task<IActionResult> PostMachines([FromBody] IEnumerable<Machine> machines)
        {
            _machineSender.SendMachines(ServiceBusMachineHelper.MapMultipleServiceBusMachineDTOs(machines.ToList(), ServiceBusEnumStatus.Create));
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
                _machineSender.SendMachine(ServiceBusMachineHelper.MapSingleServiceBusMachineDTO(machineToRemove, ServiceBusEnumStatus.Delete));
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
                _machineSender.SendMachines(ServiceBusMachineHelper.MapMultipleServiceBusMachineDTOs(machinesToRemove.ToList(), ServiceBusEnumStatus.Delete));
                _unitOfWork.Machines.RemoveRange(machinesToRemove);
                await _unitOfWork.CompleteAsync();

                return NoContent();
            }
        }

        [HttpPost("AssignParameterToMachine/{machineId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Machine>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignParameterToMachine(string machineId, [FromBody] string parameterId)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(Guid.Parse(machineId));
            var parameterToAssign = await _unitOfWork.Parameters.GetByIdAsync(Guid.Parse(parameterId));

            if (machine != null && parameterToAssign != null)
            {
                _unitOfWork.Machines.AssignParameterToMachine(machine, parameterToAssign);
                await _unitOfWork.CompleteAsync();

                return Ok();
            }
            else 
            {
                return NotFound();
            }
        }
    }
}