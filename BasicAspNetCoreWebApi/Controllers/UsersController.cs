using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicAspNetCoreWebApi.Data;
using BasicAspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace BasicAspNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return  _context.Users.ToList();
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public IActionResult  GetUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound("The user record couldn't be found.");
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("The user record couldn't be found.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            if (user==null)
            {
                return BadRequest("User is null.");
            }
            _context.Users.Add(user);
             _context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            //CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound("User is null.");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
