using Bike.Data;
using Bike.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bike.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly bikeContext _context;

        public PostController(bikeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            return post == null ? NotFound() : Ok(post);
        }




        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostBlockById(int id)
        {
             
            Post post = await (_context.Posts.Where(s => s.Id == id).FirstOrDefaultAsync<Post>());

             
            
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return post == null ? NotFound() : Ok(post);



        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost(Post post, IFormFile photo)
        {


            if (photo != null)
            {
                SaveFile(photo);
                post.Images = photo.FileName;
            }


            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);

        }

        void SaveFile(IFormFile file)
        {
            try
            {
                //var path = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                // Uncomment to save the file
                //if(!Directory.Exists(path))
                //    Directory.CreateDirectory(path);

                //using(var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName))) {
                //    file.CopyTo(fileStream);
                //}
            }
            catch
            {
                Response.StatusCode = 400;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostById(int id, Post post)
        {
            if (id != post.Id) return BadRequest();
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePostById(int id)
        {
            var deleteToPost = await _context.Posts.FindAsync(id);
            if (deleteToPost == null) return NotFound();

            _context.Posts.Remove(deleteToPost);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
