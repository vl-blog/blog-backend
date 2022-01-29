using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Server.Controllers;

[Route("api/posts")]
[ApiController]
[AllowAnonymous]
public sealed class PostsController : ControllerBase
{
    private readonly BlogContext _context;

    public PostsController(BlogContext context)
    {
        _context = context;
    }
        
    [HttpGet("getPost")]
    public async Task<ActionResult<PostResponseModel>> GetPost([FromQuery] string id)
    {
        var post = await _context.Posts
            .AsNoTracking()
            .Include(p => p.Tags)
            .Include(p => p.ReadMorePosts)
            .FirstOrDefaultAsync(p => p.Key == id);
        if (post == null)
            return NotFound();
            
        var postResponse = new PostResponseModel
        {
            Author = post.Author,
            Tags = post.Tags?.Select(t => new TagResponseModel
            {
                Id = t.Key,
                Name = t.Name
            }).ToList(),
            ImageUrl = post.ImageUrl,
            MarkdownContent = post.MarkdownContent,
            PostId = post.Key,
            PostTitle = post.PostTitle,
            LastEditedTime = post.LastEditedTime.ToString("f", new CultureInfo("en-US")),
            ReadMorePosts = post.ReadMorePosts.Select(p => new PostPreviewResponseModel
            {
                PostId = p.Key,
                ImageUrl = p.ImageUrl,
                PostTitle = p.PostTitle,
                LastEditedTime = p.LastEditedTime.ToString("f", new CultureInfo("en-US"))
            }).ToList()
        };
        return postResponse;
    }
}