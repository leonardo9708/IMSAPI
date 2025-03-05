using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DBcontext;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly ContextDB _context;

        public IncidentController(ContextDB context)
        {
            _context = context;
        }

        //Get list incidents
        [HttpGet("ListIncident")]
        public async Task<ActionResult<List<Incident>>> GetIncident()
        {
            var listIncident = await _context.Incident.ToListAsync();
            return Ok(listIncident);
        }

        //Create incident
        [HttpPost("CreateIncident")]
        public async Task<ActionResult<string>> createIncident(Incident incident)
        {
            try
            {
                _context.Incident.Add(incident);
                await _context.SaveChangesAsync();

                return Ok("Successfully created");
            }
            catch (Exception e)
            {
                return BadRequest("Error creating an incident");
            }
        }

        //Update incident
        [HttpPut("UpdateIncident/{Id}")]
        public async Task<ActionResult<List<Incident>>> updateIncident(Incident incident)
        {
            var DBData = await _context.Incident.FindAsync(incident.Id);
            if(DBData == null)
            {
                return BadRequest("Incident not found");
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.Incident.ToListAsync());
        }

        //Delete incident
        [HttpDelete]
        [Route("DeleteIncident/{Id}")]
        public async Task<ActionResult<string>> deleteIncident(int Id)
        {
            var DBData = await _context.Incident.FirstOrDefaultAsync(Ob => Ob.Id == Id);
            if(DBData == null)
            {
                return NotFound("Incident not exist");
            }

            try
            {
                _context.Incident.Remove(DBData);
                await _context.SaveChangesAsync();

                return Ok("Incident delete");
            } 
            catch (Exception e)
            {
                return BadRequest("Error delete Incident");
            }
        }
    }
}
