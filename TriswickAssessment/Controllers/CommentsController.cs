using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Data;
using TriswickAssessment.Models;

namespace TriswickAssessment.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        //getting all the comments from a post
        [HttpGet("posts/{PostId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentsModel>>> GetComments(int PostId)
        {
            return await _context.Comments.Where(c => c.PostId == PostId).ToListAsync();
        }

        //Adding the comment to a post
        [HttpPost("posts/{PostId}/comments")]
        public async Task<IActionResult> AddComment(int PostId, CommentsModel comments)
        {
            var post = await _context.Posts.FindAsync(PostId);
            if (post == null)
            {
                return NotFound();
            }

            comments.PostId = PostId;
            comments.CommentDate = DateTime.UtcNow;
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
