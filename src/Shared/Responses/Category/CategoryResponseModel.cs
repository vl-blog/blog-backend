using System.Collections.Generic;
using VovaLantsovBlog.Shared.Responses.Post;

namespace VovaLantsovBlog.Shared.Responses.Category;

public sealed record CategoryResponseModel(
    string CategoryId,
    string CategoryName,
    List<PostPreviewResponseModel> Posts);