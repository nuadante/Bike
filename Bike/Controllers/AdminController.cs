using Bike.Data;
using Bike.Helpers;
using Bike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AdminController : ControllerBase
    {

        private readonly bikeContext _context; 


        public AdminController(bikeContext context)
        {
            _context = context; 

        }


        [HttpGet]
        public async Task<IEnumerable<Admin>> GetUsers()
        {
            return await _context.Admins.ToListAsync();
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            return admin == null ? NotFound() : Ok(admin);
        }





        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAdminById(int id, Admin admin)
        {
            if (id != admin.Id) return BadRequest();
            _context.Entry(admin).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdminById(int id)
        {
            var deleteToAdmin = await _context.Admins.FindAsync(id);
            if (deleteToAdmin == null) return NotFound();

            _context.Admins.Remove(deleteToAdmin);
            await _context.SaveChangesAsync();
            return NoContent();

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(Admin _admin)
        {


            Admin admin = await (_context.Admins.Where(s => s.Username == _admin.Username).FirstOrDefaultAsync<Admin>());

            if (admin == null)
            {
                return NotFound("username yanlis");
            }
            else
            {
                bool verified = BCrypt.Net.BCrypt.Verify(_admin.PasswordHass, admin.PasswordHass);

                if (!verified)
                {
                    return NotFound("sifre yanlis");
                }

                return Ok(admin);
            }


        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAdmin(Admin _admin)
        {
            _admin.PasswordHass = BCrypt.Net.BCrypt.HashPassword(_admin.PasswordHass);
            _admin.PasswordSalt = BCrypt.Net.BCrypt.HashPassword(_admin.PasswordHass);
            await _context.Admins.AddAsync(_admin);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAdminById), new { id = _admin.Id }, _admin);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Admin), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdminBlockById(int id)
        {

            Admin admin = await (_context.Admins.Where(s => s.Id == id).FirstOrDefaultAsync<Admin>());



            _context.Entry(admin).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return admin == null ? NotFound() : Ok(admin);



        }
    }
}
