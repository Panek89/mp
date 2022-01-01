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

        [HttpGet]
        public IActionResult GetParameters()
        {
            var parameters = _unitOfWork.Parameters.GetAll();
            return Ok(parameters);
        }

        [HttpPost]
        public IActionResult PostParameter(Parameter parameter)
        {
            _unitOfWork.Parameters.Add(parameter);
            _unitOfWork.Complete();
            
            return Ok(parameter);
        }
    }
}