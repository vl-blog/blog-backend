using System.Collections.Generic;

namespace VovaLantsovBlog.Shared.Responses.Post
{
    public sealed record PostResponseModel
    {
        public string PostId { get; init; }
        public string PostTitle { get; init; }
        public string ImageUrl { get; init; }
        public string LastEditedTime { get; init; }
        public string MarkdownContent { get; init; }
        public string Author { get; init; }
        public List<PostPreviewResponseModel> ReadMorePosts { get; init; }
        public List<TagResponseModel>? Tags { get; init; }
    }
}