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
    public async Task<ActionResult<CategoryResponseModel?>> GetCategoryOrTagByKey([FromQuery] string key)
    {
        var category = await _context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Key == key);
        
        if (category == null)
            return (CategoryResponseModel?) null;

        return new CategoryResponseModel(
            CategoryId: category.Key,
            CategoryName: category.Name,
            Posts: await _context.Posts
                .AsNoTracking()
                .Include(p => p.Tags)
                .Where(p => p.Tags!.Contains(category))
                .Select(p => new PostPreviewResponseModel(
                    p.Key,
                    p.PostTitle,
                    p.LastEditedTime.ToShortDateString(),
                    p.ImageUrl))
                .ToListAsync());
    }
}