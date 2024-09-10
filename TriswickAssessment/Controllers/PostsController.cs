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
                .Include(p => p.Comments)
                .Include(p => p.Tags)
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

            post.LikeCount += 1; // Increment the LikeCount by 1
            post.DateUpdated = DateTime.Now; // Update the date

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent(); // Return 204 status code to indicate success without returning data
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


        // Add Comment to Post
        //[HttpPost("{postId}/comments")]
        //public async Task<ActionResult<CommentsModel>> AddComment(int postId, [FromBody] CommentsModel comment)
        //{
        //    // Check if the post exists
        //    var post = await _context.Posts.FindAsync(postId);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    // Set the PostId and CommentDate
        //    comment.PostId = postId;
        //    comment.CommentDate = DateTime.Now;

        //    // Add comment to the database
        //    _context.Comments.Add(comment);
        //    await _context.SaveChangesAsync();

        //    // Return the created comment with a location header
        //    return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        //}

        [HttpPost("{postId}/comments")]
        public async Task<ActionResult<CommentsModel>> AddComment(int postId, [FromBody] CommentsModel comment)
        {
            // Check if the post exists
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                return NotFound();
            }

            // Set the PostId and CommentDate
            comment.PostId = postId;
            comment.CommentDate = DateTime.Now;

            // Add comment to the database
            _context.Comments.Add(comment);

            // Also add the comment to the post's Comments collection
            post.Comments.Add(comment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle exception (logging, etc.)
                return StatusCode(500, "A problem happened while handling your request.");
            }

            // Return the created comment with a location header
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




    }
}
