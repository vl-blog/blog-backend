using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace VovaLantsovBlog.Shared.Models;

[Table("posts", Schema = Constants.SchemaName)]
public sealed class Post
{
    [Key, Required, Column("post_key", TypeName = "text")]
    public string Key { get; set; }
    [Required, Column("post_title", TypeName = "text")]
    public string PostTitle { get; set; }
    [Required, Column("post_image_url", TypeName = "text")]
    public string ImageUrl { get; set; }
    [Required, Column("post_last_edited"), DataType(DataType.DateTime)]
    public DateTime LastEditedTime { get; set; }
    [Required, Column("post_markdown_content", TypeName = "text")]
    public string MarkdownContent { get; set; }
    [Required, Column("post_author", TypeName = "text")]
    public string Author { get; set; }
    public List<Post>? ReadMorePosts { get; set; }
    public List<Tag>? Tags { get; set; }
}