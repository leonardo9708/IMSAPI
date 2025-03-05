using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DBcontext;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ContextDB _context;

        public UsersController(ContextDB context)
        {
            _context = context;
        }

        //Create user
        [HttpPost("CreateUser")]
        public async Task<ActionResult<string>> createUser(Users user)
        {
            try
            {
                _context.SupportUsers.Add(user);
                await _context.SaveChangesAsync();

                return Ok("Successfully created");
            }
            catch (Exception e)
            {
                return BadRequest("Error creating an user");
            }
        }

        //Get list users
        [HttpGet("ListUsers")]
        public async Task<ActionResult<List<Users>>> GetUser()
        {
            var listUsers = await _context.SupportUsers.ToListAsync();
            return Ok(listUsers);
        }

        [HttpGet]
        [Route("Consult/{Name}")]
        public async Task<ActionResult<List<Users>>> SearcheUser(string Name)
        {
            var listUsers = await _context.SupportUsers.FirstOrDefaultAsync(ob => ob.Name == Name);
            if(listUsers == null)
            {
                return NotFound("No matches");
            }

            return Ok(listUsers);
        }

        //Update user
        [HttpPut("UpdateUser/{Id}")]
        public async Task<ActionResult<List<Users>>> updateUser(Users user)
        {
            var DBData = await _context.SupportUsers.FindAsync(user.Id);
            if (DBData == null)
            {
                return BadRequest("User not found");
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.Incident.ToListAsync());
        }

        //Delete User
        [HttpDelete]
        [Route("DeleteUser/{Id}")]
        public async Task<ActionResult<string>> deleteUser(int Id)
        {
            var DBData = await _context.SupportUsers.FirstOrDefaultAsync(Ob => Ob.Id == Id);
            if (DBData == null)
            {
                return NotFound("Users not exist");
            }

            try
            {
                _context.SupportUsers.Remove(DBData);
                await _context.SaveChangesAsync();

                return Ok("User delete");
            }
            catch (Exception e)
            {
                return BadRequest("Error delete user");
            }
        }
    }
}
