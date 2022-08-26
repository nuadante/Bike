using Bike.Data;
using Bike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bike.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly bikeContext _context;

        public RouteController(bikeContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<IEnumerable<Route>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Route), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRouteById(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            return route == null ? NotFound() : Ok(route);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoute(Route route)
        {
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRouteById), new { id = route.Id }, route);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRouteById(int id, Route route)
        {
            if (id != route.Id) return BadRequest();
            _context.Entry(route).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRouteById(int id)
        {
            var deleteToRoute = await _context.Routes.FindAsync(id);
            if (deleteToRoute == null) return NotFound();

            _context.Routes.Remove(deleteToRoute);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
