using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VovaLantsovBlog.Shared.Models
{
    [Table("tags", Schema = Constants.SchemaName)]
    public sealed class Tag
    {
        [Key, Required, Column("tag_key", TypeName = "text")]
        public string Key { get; set; } = null!;

        [Required, Column("tag_name", TypeName = "text")]
        public string Name { get; set; } = null!;
        
        [Required, Column("is_category", TypeName = "boolean")]
        public bool IsCategory { get; set; }
    }
}