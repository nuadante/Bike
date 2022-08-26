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
    public class PostCommentController : ControllerBase
    {
        private readonly bikeContext _context;

        public PostCommentController(bikeContext context)
        {
            _context = context;
        } 

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostComment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostCommentById(int id)
        {
            PostComment postComment = await _context.PostComments.Where(x => x.PostId == id && x.IsActived).ToListAsync();

            return postComment == null ? NotFound() : Ok(postComment);
           
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostComment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostCommentsById(int id)
        {
            var comments = (from O in _context.PostComments
                            where EF.Functions.Like(O.PostId.ToString(), id.ToString())
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
        public async Task<IActionResult> CreatePostComment(PostComment postComment)
        {
            await _context.PostComments.AddAsync(postComment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostCommentById), new { id = postComment.Id }, postComment);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostCommentById(int id, PostComment postComment)
        {
            if (id != postComment.Id) return BadRequest();
            _context.Entry(postComment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePostCommentById(int id)
        {
            var deleteToPostComment = await _context.PostComments.FindAsync(id);
            if (deleteToPostComment == null) return NotFound();

            _context.PostComments.Remove(deleteToPostComment);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
