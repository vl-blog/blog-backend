using System.Collections.Generic;

namespace VovaLantsovBlog.Shared.Responses.Post;

public sealed record PostResponseModel(
    string PostId,
    string PostTitle,
    string ImageUrl,
    string LastEditedTime,
    string MarkdownContent,
    string Author,
    List<PostPreviewResponseModel> ReadMorePosts,
    List<TagResponseModel>? Tags);