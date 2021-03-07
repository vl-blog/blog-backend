using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VovaLantsovBlog.Shared.Responses.Category;

namespace VovaLantsovBlog.Server.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [AllowAnonymous]
    public sealed class CategoriesController : ControllerBase
    {
        [HttpGet("getCategory")]
        public ActionResult<CategoryResponseModel> GetCategory([FromQuery] string tag)
        {
            var categories = new HomeController().GetCategoriesPreview().Value;
            var category = categories.FirstOrDefault(c => c.CategoryId == tag);
            return category!;
        }
    }
}