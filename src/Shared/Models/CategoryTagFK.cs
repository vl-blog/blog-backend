using System.ComponentModel.DataAnnotations.Schema;

namespace VovaLantsovBlog.Shared.Models
{
    [Table("categories_tags_fk", Schema = Constants.SchemaName)]
    // ReSharper disable once InconsistentNaming
    public sealed class CategoryTagFK
    {
        public string CategoryKey { get; set; } = null!;
        [ForeignKey("CategoryKey")]
        public Category Category { get; set; } = null!;

        public string TagKey { get; set; } = null!;
        [ForeignKey("TagKey")]
        public Tag Tag { get; set; } = null!;
    }
}