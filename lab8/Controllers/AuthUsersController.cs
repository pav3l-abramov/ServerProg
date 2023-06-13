using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroes.Models;

namespace SuperHeroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly AuthenticationContex _context;

        public AuthUsersController(AuthenticationContex context)
        {
            _context = context;
        }

        // GET: api/AuthUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthenticationUser>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [Authorize(Roles = "pavel")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthenticationUser>> GetUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "pavel")]
        [HttpPost]
        public async Task<ActionResult<AuthenticationUser>> PostUser(AuthenticationUser user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthContext.Users'  is null.");
            }

            if (!user.Role.Equals("pavel") && !user.Role.Equals("user"))
            {
                return Problem("Wrong role! You can be pavel or user");
            }

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthenticationUserExists(user.Login))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Login }, user);
        }

        // DELETE: api/Users/5
        [Authorize(Roles = "pavel")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthenticationUserExists(string id)
        {
            return (_context.Users?.Any(e => e.Login == id)).GetValueOrDefault();
        }
    }
}
