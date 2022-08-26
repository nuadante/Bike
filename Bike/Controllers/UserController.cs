using Bike.Data;
using Bike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly bikeContext _context;

        public UserController(bikeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(User user)
        {

            user.PasswordHass = BCrypt.Net.BCrypt.HashPassword(user.PasswordHass);
            user.PasswordSalt = BCrypt.Net.BCrypt.HashPassword(user.PasswordHass);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(User _user)
        {


            User user = await (_context.Users.Where(s => s.Username == _user.Username).FirstOrDefaultAsync<User>());

            if (user == null)
            {
                return NotFound("username yanlis");
            }
            else
            {


                bool verified = BCrypt.Net.BCrypt.Verify(_user.PasswordHass, user.PasswordHass);

                if (!verified)
                {
                    return NotFound("sifre yanlis");
                }

                return Ok(user);
            }

             
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserById(int id,User user)
        {
            if (id != user.Id) return BadRequest();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var deleteToUser = await _context.Users.FindAsync(id);
            if (deleteToUser == null) return NotFound();

            _context.Users.Remove(deleteToUser);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBlockById(int id)
        {

            User user = await (_context.Users.Where(s => s.Id == id).FirstOrDefaultAsync<User>());
           


            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user == null ? NotFound() : Ok(user);



        }
    }
}
