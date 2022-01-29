using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Shared.Responses.Category;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Server.Controllers;

[Route("api/tags")]
[ApiController]
[AllowAnonymous]
public sealed class TagsController : ControllerBase
{
    private readonly BlogContext _context;

    public TagsController(BlogContext context)
    {
        _context = context;
    }

    [HttpGet("getTag")]
    public ActionResult<CategoryResponseModel>? GetCategoryOrTagByKey([FromQuery] string key)
    {
        var category = _context.Tags.FirstOrDefault(t => t.Key == key);
        if (category == null)
            return null;
        return new CategoryResponseModel
        {
            CategoryId = category!.Key,
            CategoryName = category.Name,
            Posts = _context.Posts
                .Include(p => p.Tags)
                .Where(p => p.Tags!.Contains(category))
                .Select(p => new PostPreviewResponseModel
                {
                    ImageUrl = p.ImageUrl,
                    PostId = p.Key,
                    PostTitle = p.PostTitle,
                    LastEditedTime = p.LastEditedTime.ToShortDateString()
                })
                .ToList()
        };
    }
}