namespace VovaLantsovBlog.Shared.Responses.Post;

public sealed record PostPreviewResponseModel
{
    public string PostId { get; init; }
    public string PostTitle { get; init; }
    public string LastEditedTime { get; init; }
    public string ImageUrl { get; init; }
}