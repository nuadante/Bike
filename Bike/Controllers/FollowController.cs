using Bike.Data;
using Bike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bike.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly bikeContext _context;

        public FollowController(bikeContext context)
        {
            _context = context;
        }


         

        [HttpPost]
        [ProducesResponseType(typeof(Follow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowersById(int id)
        {
             
            
            

            var follow = (from O in _context.Follows
                          join OD in _context.Users on O.FollowerId equals OD.Id
                          where EF.Functions.Like(O.FollowerId.ToString(), id.ToString())
                          select new
                          {
                              OD.Username,
                              OD.Id,
                              OD.Email,
                              OD.Firstname
                          }).Distinct().ToList();

            return follow == null ? NotFound() : Ok(follow);
        }
     


        [HttpPost]
        [ProducesResponseType(typeof(Follow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowingById(int id)
        {

            var follow = (from O in _context.Follows
                          join OD in _context.Users on O.FollowerId equals OD.Id
                          where EF.Functions.Like(O.UserId.ToString(), id.ToString())
                          select new
                          {
                              OD.Username,
                              OD.Id,
                              OD.Email,
                              OD.Firstname
                          }).Distinct().ToList();

            return follow == null ? NotFound() : Ok(follow);
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Following(int _followerId, int _userId)
        {
            // takip sorgulama
            var following = await (_context.Follows.Where(s => s.UserId == _userId && s.FollowerId == _followerId).FirstOrDefaultAsync<Follow>());

            //eger veritabaninda boyle bir sey yoksa takip et
            if (following == null) {

                Follow add = new Follow()
                {
                    UserId = _userId,
                    FollowerId = _followerId
                };
                await _context.Follows.AddAsync(add);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetFollowersById), new { id = add.Id }, add);
            }

            //veritabaninda var, takip ediyor, takipten cik 
            else if (following.IsActived== true)
            {
                following.IsActived = false;
                _context.Entry(following).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            //veritabaninda var, takip etmiyor, takip et
            else if (following.IsActived == false)
            {
                following.IsActived = true;
                _context.Entry(following).State = EntityState.Modified;
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
