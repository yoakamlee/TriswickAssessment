using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Data;
using TriswickAssessment.Models;

namespace TriswickAssessment.Controllers
{
    //probably change route name later? - yls 
    [Route("api/[controller]")]
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
            //TODO: Get comments after making comment controller - yls 
            var posts = await _context.Posts.ToListAsync();

            return Ok(posts);
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

        //TODO: Rest of post get/post request - yls
    }
}
