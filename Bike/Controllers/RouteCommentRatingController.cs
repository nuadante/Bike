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
    public class RouteCommentRatingController : ControllerBase
    {
        private readonly bikeContext _context;

        public RouteCommentRatingController(bikeContext context)
        {
            _context = context;
        }







        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RouteCommentRating), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLikeById(int _commentId, bool like)
        {
            RouteCommentRating commentLikes;
            if (like)
            {
                commentLikes = await _context.RouteCommentRatings.Where(x => x.RouteCommentId == _commentId && x.RatingLike).ToListAsync();

            }
            else
            {
                commentLikes = await _context.RouteCommentRatings.Where(x => x.RouteCommentId == _commentId && x.RatingLike).ToListAsync();

            }
            User user = await _context.Users.FindAsync(commentLikes.Id);

            return like == null && user == null ? NotFound() : Ok(user);
        }

         


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Ratings(int _commentId, int _userId, bool like)
        {
            // sorgulama
            RouteCommentRating ratings = await (_context.RouteCommentRatings.Where(s => s.UserId == _userId && s.RouteCommentId == _commentId).FirstOrDefaultAsync<RouteCommentRating>());

            //eger veritabaninda boyle bir sey yoksa islem yap
            if (ratings == null)
            {

                RouteCommentRating add = new RouteCommentRating()
                {
                    UserId = _userId,
                    RouteCommentId = _commentId,
                    RatingLike = like,
                    RatingUnlike = !like


                };
                await _context.RouteCommentRatings.AddAsync(add);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLikeById), new { id = add.Id }, add);
            }

            // daha onceden begenmis, simdi begenisini cekiyor ya da begenmiyor
            else if (ratings.RatingLike == true)
            {
                if (like == true)
                {
                    ratings.RatingUnlike = false;
                    ratings.RatingLike = false;
                }
                else
                {
                    ratings.RatingUnlike = true;
                    ratings.RatingLike = false;
                }
                _context.Entry(ratings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            // daha onceden begenmemis, simdi begenmemisini cekiyor yada begeniyor
            else if (ratings.RatingLike == false)
            {
                if (like == false)
                {
                    ratings.RatingUnlike = false;
                    ratings.RatingLike = false;
                }
                else
                {
                    ratings.RatingUnlike = false;
                    ratings.RatingLike = true;
                }

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
