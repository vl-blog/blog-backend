using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Server.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [AllowAnonymous]
    public sealed class PostsController : ControllerBase
    {
        [HttpGet("getPost")]
        public ActionResult<PostResponseModel> GetPost([FromQuery] string id)
        {
            const string link = "https://images.unsplash.com/photo-1612832020988-e55e474cfa21?ixid=MXwxMjA3fDF8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=1350&q=80";
            var post = new PostResponseModel
            {
                Author = "Vova Lantsov",
                ImageUrl = "images/blog-image.png",
                PostId = id,
                PostTitle = "Post title",
                LastEditedTime = "5 hours ago",
                MarkdownContent = "# Markdown syntax guide\n\n## Headers\n\n# This is a Heading h1\n## This is a Heading h2 \n###### This is a Heading h6\n\n## Emphasis\n\n*This text will be italic*  \n_This will also be italic_\n\n**This text will be bold**  \n__This will also be bold__\n\n_You **can** combine them_\n\n## Lists\n\n### Unordered\n\n* Item 1\n* Item 2\n* Item 2a\n* Item 2b\n\n### Ordered\n\n1. Item 1\n1. Item 2\n1. Item 3\n  1. Item 3a\n  1. Item 3b\n\n## Images\n\n![This is a alt text.](images/img-1.png \"This is a sample image.\")\n\n## Links\n\nYou may be using [Markdown Live Preview](https://markdownlivepreview.com/).\n\n## Blockquotes\n\n> Markdown is a lightweight markup language with plain-text-formatting syntax, created in 2004 by John Gruber with Aaron Swartz.\n>\n>> Markdown is often used to format readme files, for writing messages in online discussion forums, and to create rich text using a plain text editor.\n\n## Blocks of code\n\n```\nlet message = 'Hello world';\nalert(message);\n```\n\n## Inline code\n\nThis web site is using `markedjs/marked`.",
                ReadMorePosts = new List<PostPreviewResponseModel>
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
            };
            return post;
        }
    }
}