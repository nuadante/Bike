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
    public class PostLikeController : ControllerBase
    {
        private readonly bikeContext _context;

        public PostLikeController(bikeContext context)
        {
            _context = context;
        }

         


        [HttpGet]
        [ProducesResponseType(typeof(PostLike), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLikesById(int _postId)
        { 


            var likes = (from O in _context.PostLikes
                          join OD in _context.Users on O.UserId equals OD.Id
                          where EF.Functions.Like(O.PostId.ToString(), _postId.ToString())
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
        public async Task<IActionResult> GetLike(int _postId, int _userId, bool like)
        {
            // sorgulama
            PostLike ratings = await (_context.PostLikes.Where(s => s.UserId == _userId && s.PostId == _postId).FirstOrDefaultAsync<PostLike>());

            //eger veritabaninda boyle bir sey yoksa islem yap
            if (ratings == null)
            {

                PostLike add = new PostLike()
                {
                    UserId = _userId,
                    PostId = _postId,
                    RatingLike = like

                };
                await _context.PostLikes.AddAsync(add);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLikesById), new { id = add.Id }, add);
            }

            // daha onceden begenmis, begenisi cekiyor
            else if (ratings.RatingLike == true)
            {
                ratings.RatingLike = false;
                _context.Entry(ratings).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            // daha onceden begenmemis, begeniyor
            else if (ratings.RatingLike == false)
            {

                ratings.RatingLike = true;
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

        public static implicit operator PostLikeController(List<PostLike> v)
        {
            throw new NotImplementedException();
        }
    }
}
