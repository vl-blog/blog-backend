using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace VovaLantsovBlog.Shared.Models;

[Table("tags", Schema = Constants.SchemaName)]
public sealed class Tag
{
    [Key, Required, Column("tag_key", TypeName = "text")]
    public string Key { get; set; }

    [Required, Column("tag_name", TypeName = "text")]
    public string Name { get; set; }
        
    [Required, Column("is_category", TypeName = "boolean")]
    public bool IsCategory { get; set; }

    public List<Post>? Posts { get; set; }
}