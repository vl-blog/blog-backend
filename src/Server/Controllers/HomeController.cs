using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VovaLantsovBlog.Data;
using VovaLantsovBlog.Shared.Responses.Category;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Server.Controllers;

[Route("api/home")]
[ApiController]
[AllowAnonymous]
public sealed class HomeController : ControllerBase
{
    private readonly BlogContext _context;

    public HomeController(BlogContext context)
    {
        _context = context;
    }
        
    [HttpGet("getCategoriesPreview")]
    public ActionResult<List<CategoryResponseModel>> GetCategoriesPreview([FromQuery] bool includePosts = true)
    {
        var models = new List<CategoryResponseModel>();
        var query = _context.Tags.Where(t => t.IsCategory);
        if (includePosts)
            query = query.Include(t => t.Posts);
        var categories = query.ToArray();
        foreach (var category in categories)
        {
            var model = new CategoryResponseModel
            {
                CategoryId = category.Key,
                CategoryName = category.Name,
                Posts = new List<PostPreviewResponseModel>()
            };
            if (includePosts)
            {
                foreach (var post in category.Posts)
                {
                    model.Posts.Add(new PostPreviewResponseModel
                    {
                        ImageUrl = post.ImageUrl,
                        PostId = post.Key,
                        PostTitle = post.PostTitle,
                        LastEditedTime = post.LastEditedTime.ToLongDateString()
                    });
                }
            }
            models.Add(model);
        }

        return models;
    }
}