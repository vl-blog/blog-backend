using Microsoft.EntityFrameworkCore.Migrations;

namespace VovaLantsovBlog.Data.Migrations
{
    public partial class ManyToManyPostsTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tags_posts_PostKey",
                schema: "public",
                table: "tags");

            migrationBuilder.DropIndex(
                name: "IX_tags_PostKey",
                schema: "public",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "PostKey",
                schema: "public",
                table: "tags");

            migrationBuilder.CreateTable(
                name: "PostTag",
                schema: "public",
                columns: table => new
                {
                    PostsKey = table.Column<string>(type: "text", nullable: false),
                    TagsKey = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsKey, x.TagsKey });
                    table.ForeignKey(
                        name: "FK_PostTag_posts_PostsKey",
                        column: x => x.PostsKey,
                        principalSchema: "public",
                        principalTable: "posts",
                        principalColumn: "post_key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_tags_TagsKey",
                        column: x => x.TagsKey,
                        principalSchema: "public",
                        principalTable: "tags",
                        principalColumn: "tag_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsKey",
                schema: "public",
                table: "PostTag",
                column: "TagsKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "PostKey",
                schema: "public",
                table: "tags",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tags_PostKey",
                schema: "public",
                table: "tags",
                column: "PostKey");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_posts_PostKey",
                schema: "public",
                table: "tags",
                column: "PostKey",
                principalSchema: "public",
                principalTable: "posts",
                principalColumn: "post_key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
