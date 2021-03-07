using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VovaLantsovBlog.Shared.Responses.Category;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Server.Controllers
{
    [Route("api/home")]
    [ApiController]
    [AllowAnonymous]
    public sealed class HomeController : ControllerBase
    {
        [HttpGet("getCategoriesPreview")]
        public ActionResult<List<CategoryResponseModel>> GetCategoriesPreview()
        {
            const string link = "https://images.unsplash.com/photo-1612832020988-e55e474cfa21?ixid=MXwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";
            return new List<CategoryResponseModel>
            {
                new()
                {
                    CategoryId = "dotnet",
                    CategoryName = ".NET / C#",
                    Posts = new List<PostPreviewResponseModel>
                    {
                        new()
                        {
                            PostId = "1",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 1"
                        },
                        new()
                        {
                            PostId = "2",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 2"
                        },
                        new()
                        {
                            PostId = "3",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 3"
                        },
                        new()
                        {
                            PostId = "4",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 4"
                        },
                        new()
                        {
                            PostId = "5",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 5"
                        }
                    }
                },
                new()
                {
                    CategoryId = "rpi",
                    CategoryName = "Raspberry Pi",
                    Posts = new List<PostPreviewResponseModel>
                    {
                        new()
                        {
                            PostId = "6",
                            LastEditedTime = "1 hour ago",
                            ImageUrl = link,
                            PostTitle = "Post 6"
                        }
                    }
                },
                new()
                {
                    CategoryId = "tourism",
                    CategoryName = "Tourism"
                }
            };
        }
    }
}