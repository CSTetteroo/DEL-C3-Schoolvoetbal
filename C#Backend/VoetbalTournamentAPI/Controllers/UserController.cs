using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VoetbalTournamentAPI.Model;
using VoetbalTournamentAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VoetbalTournamentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(VoetbalTournamentDataContext context, IPasswordHasher<User> passwordHasher) : ControllerBase
    {
        private readonly VoetbalTournamentDataContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User with this email already exists");
            }

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = _passwordHasher.HashPassword(existingUser, user.Password);
            existingUser.Admin = user.Admin;
            existingUser.Team_id = user.Team_id;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginUser.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginUser.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok("Login successful");
        }
    }
}
