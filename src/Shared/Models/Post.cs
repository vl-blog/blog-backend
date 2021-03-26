using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VovaLantsovBlog.Shared.Models
{
    [Table("posts", Schema = Constants.SchemaName)]
    public sealed record Post
    {
        [Key, Required, Column("post_key", TypeName = "text")]
        public string Key { get; init; }
        [Required, Column("post_title", TypeName = "text")]
        public string PostTitle { get; init; }
        [Required, Column("post_image_url", TypeName = "text")]
        public string ImageUrl { get; init; }
        [Required, Column("post_last_edited"), DataType(DataType.DateTime)]
        public DateTime LastEditedTime { get; init; }
        [Required, Column("post_markdown_content", TypeName = "text")]
        public string MarkdownContent { get; init; }
        [Required, Column("post_author", TypeName = "text")]
        public string Author { get; init; }
        public List<Post> ReadMorePosts { get; init; }
        public List<Tag>? Tags { get; init; }
    }
}