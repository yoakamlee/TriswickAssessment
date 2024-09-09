using Microsoft.AspNetCore.Authorization;
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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
        //{
        //    return await _context.Posts.Include(p => p.Comments).Include(p => p.LikeCount).ToListAsync();
        //}

        [HttpPost]
        //[Authorize(Roles = "Regular,Moderator")]
        public async Task<ActionResult<PostModel>> CreatePost(PostModel post)
        {

            if (await _context.Posts.AnyAsync(p => p.Id == post.Id))
            {
                return Conflict("Post with the same ID already exists.");
            }

            post.DateCreated = DateTime.Now;
            post.DateUpdated = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllPosts), new { id = post.Id });
        }

        //UPDATE: completed - yls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)  // Include related comments
                .Include(p => p.Tags)      // Include related tags
                .ToListAsync();

            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            return Ok(posts);
        }


        //Like pots
        [HttpPost("{id}/like/{userId}")]
        public async Task<IActionResult> LikePost(int id, string userId)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var alreadyLiked = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == id && l.UserId == userId);

            if (alreadyLiked != null)
            {
                return Conflict("You have already liked this post");
            }

            if (post.OriginalPostId == userId)
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

        //Clear Post !!! For testing purposes only
        [HttpDelete("clearTestPosts")]
        public async Task<IActionResult> ClearTestPosts()
        {
            var testPosts = await _context.Posts
                .Where(p => p.OriginalPostId.StartsWith("test"))
                .ToListAsync();

            if (!testPosts.Any())
            {
                return NotFound("No test posts found.");
            }

            _context.Posts.RemoveRange(testPosts);
            await _context.SaveChangesAsync();

            return Ok("Test posts deleted successfully.");
        }


        //Unlike posts
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

        //Moderator Ability: Tag Post for misleading
        [HttpPost("{id}/tag/{tag}")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> TagPost(int id, string tag)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var tagModel = new TagModel
            {
                Tag = tag,
                PostId = post.Id
            };

            _context.Tags.Add(tagModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
