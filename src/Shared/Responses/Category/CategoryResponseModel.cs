using System.Collections.Generic;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Shared.Responses.Category
{
    public sealed record CategoryResponseModel
    {
        public string CategoryName { get; init; }
        public string CategoryId { get; init; }
        public List<PostPreviewResponseModel> Posts { get; init; }
    }
}