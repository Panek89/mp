using Microsoft.AspNetCore.Mvc;

namespace MP.MachinesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAllMachines()
        {
            return Ok();
        }

        // [HttpGet("{id}")]
        // public ActionResult<Machine> GetMachineById(string id)
        // {
            
        //     return Ok();
        // }

        // [HttpPost]
        // public ActionResult<Machine> PostMachine(Machine machine)
        // {
        //     return CreatedAtAction(nameof(GetAllMachines), new { id = machine.Id }, machine);
        // }
    }
}