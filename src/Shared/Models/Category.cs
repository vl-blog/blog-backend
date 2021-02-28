using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VovaLantsovBlog.Shared.Models
{
    [Table("categories", Schema = Constants.SchemaName)]
    public sealed class Category
    {
        [Key, Required, Column("category_key", TypeName = "text")]
        public string Key { get; set; } = null!;

        [Required, Column("category_name", TypeName = "text")]
        public string Name { get; set; } = null!;
    }
}