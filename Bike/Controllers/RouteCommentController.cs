using Bike.Data;
using Bike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RouteCommentController : ControllerBase
    {
        private readonly bikeContext _context;

        public RouteCommentController(bikeContext context)
        {
            _context = context;
        } 

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RouteComment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRouteCommentById(int id)
        {
            var routeComment = await _context.RouteComments.FindAsync(id);
            return routeComment == null ? NotFound() : Ok(routeComment);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RouteComment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRouteCommentsById(int id)
        {

            var comments = (from O in _context.RouteComments
                            where EF.Functions.Like(O.RouteId.ToString(), id.ToString())
                            select new
                            {
                                O.UserId,
                                O.Description,
                                O.Id

                            }).Distinct().ToList();

            return comments == null ? NotFound() : Ok(comments);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRouteComment(RouteComment routeComment)
        {
            await _context.RouteComments.AddAsync(routeComment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRouteCommentById), new { id = routeComment.Id }, routeComment);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRouteCommentById(int id, RouteComment routeComment)
        {
            if (id != routeComment.Id) return BadRequest();
            _context.Entry(routeComment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRouteCommentById(int id)
        {
            var deleteToRouteComment = await _context.RouteComments.FindAsync(id);
            if (deleteToRouteComment == null) return NotFound();

            _context.RouteComments.Remove(deleteToRouteComment);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
