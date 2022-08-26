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
    public class RouteRatingController : ControllerBase
    {
        private readonly bikeContext _context;

        public RouteRatingController(bikeContext context)
        {
            _context = context;
        }







        [HttpGet]
        [ProducesResponseType(typeof(RouteRating), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRatings(int _routeId)
        {
            var likes = (from O in _context.RouteRatings
                         join OD in _context.Users on O.UserId equals OD.Id
                         where EF.Functions.Like(O.RouteId.ToString(), _routeId.ToString())
                         select new
                         {
                             OD.Username,
                             OD.Id,
                             OD.Email,
                             OD.Firstname
                         }).Distinct().ToList();

            return likes == null ? NotFound() : Ok(likes);
        }

         


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostRating(int _routeId, int _userId, int like)
        {
            // sorgulama
            RouteRating ratings = await (_context.RouteRatings.Where(s => s.UserId == _userId && s.RouteId == _routeId).FirstOrDefaultAsync<RouteRating>());

            //eger veritabaninda boyle bir sey yoksa islem yap
            if (ratings == null)
            {

                RouteRating add = new RouteRating()
                {
                    UserId = _userId,
                    RouteId = _routeId,
                    Rating = like


                };
                await _context.RouteRatings.AddAsync(add);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetRatings), new { id = add.Id }, add);
            }

            // daha onceden yildiz vermis, ayni yildizi veriyor, bu da yildizi cekiyor demek
            else if (ratings.Rating == like || like == 0)
            {
                
                _context.Entry(ratings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            // baska bir yildiz veriyor
            else if (ratings.Rating != like)
            {
                ratings.Rating = like;
                ratings.IsActived = true;

                _context.Entry(ratings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            //hata gonder
            else
            {
                return NotFound();
            }



        }


    }
}
