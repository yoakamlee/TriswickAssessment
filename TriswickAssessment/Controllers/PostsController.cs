using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Data;
using TriswickAssessment.Models;

namespace TriswickAssessment.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
        {
            //DONE: Get comments after making comment controller - yls 
           return await _context.Posts.Include(p => p.Comments).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> CreatePost(PostModel post)
        {
            post.DateCreated = DateTime.Now;
            post.DateUpdated = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPosts), new {id = post.Id});
        }

        //UPDATE: uncompleted neeed to add authentication controller - yls
        [HttpGet("{id}")]
        public async Task<ActionResult<PostModel>> GetPost(int id)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }


        //UPDATE: add User auth - yls
        [HttpPost("{id}/like/{userId}")]
        public async Task<IActionResult> LikePost(int id, string userId)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var alreadyLiked = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == id && l.UserId == userId);

            if(alreadyLiked != null)
            {
                return Conflict("You have already liked this post");
            }

            if(post.OriginalPostId == userId)
            {
                return BadRequest("Unable to like post");
            }

            var like = new LikesModel
            {
                PostId = post.Id,
                UserId = userId,
            };

            _context.Likes.Add(like);
            post.LikeCount++;
            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpPost("{id}/unlike/{userId}")]
        public async Task<IActionResult> UnlikePost(int id, string userId)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == id && l.UserId == userId);
            if (like == null)
            {
                return NotFound("Like not found.");
            }

            _context.Likes.Remove(like);
            post.LikeCount--;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
