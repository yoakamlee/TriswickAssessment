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

        //Create a post
        [HttpPost]
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

        //Fetch all Posts 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .OrderByDescending(p => p.DateUpdated)
                .ToListAsync();

            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostModel>> GetPostById(int postId)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        //Add Like
        [HttpPut("UpdateLikes/{id}")]
        public async Task<IActionResult> UpdateLikeCount(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            post.LikeCount += 1;
            post.DateUpdated = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                return Ok("Like added");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        //Dislike 
        [HttpPut("Unlike/{id}")]
        public async Task<IActionResult> UnlikePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            // cant go < 0
            if (post.LikeCount > 0)
            {
                post.LikeCount -= 1;
            }

            post.DateUpdated = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();

                return Ok("Like removed");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
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

        [HttpPost("{postId}/comments")]
        public async Task<ActionResult<CommentsModel>> AddComment(int postId, [FromBody] CommentsModel comment)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }

            comment.PostId = postId;
            comment.CommentDate = DateTime.Now;

            _context.Comments.Add(comment);

            post.Comments.Add(comment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }




        // Get a specific comment by ID
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<CommentsModel>> GetCommentById(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            return Ok(comment);
        }

        //Get tags
        [HttpPost("{postId}/tags")]
        public async Task<ActionResult<TagModel>> AddTag(int postId, [FromBody] TagModel tag)
        {
            // Check if the post exists
            var post = await _context.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }

            post.Tags.Add(tag);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            // Return the added tag with a location header
            return CreatedAtAction(nameof(GetTagById), new { id = tag.Id }, tag);
        }

        [HttpGet("tags/{id}")]
        public async Task<ActionResult<TagModel>> GetTagById(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            return Ok(tag);
        }





    }
}
