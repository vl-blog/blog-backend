namespace VovaLantsovBlog.Shared.Responses.Post;

public sealed record PostPreviewResponseModel(
    string PostId,
    string PostTitle,
    string LastEditedTime,
    string ImageUrl);