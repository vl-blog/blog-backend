using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VovaLantsovBlog.Data.Migrations
{
    public partial class PostAndTagModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "posts",
                schema: "public",
                columns: table => new
                {
                    post_key = table.Column<string>(type: "text", nullable: false),
                    post_title = table.Column<string>(type: "text", nullable: false),
                    post_image_url = table.Column<string>(type: "text", nullable: false),
                    post_last_edited = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    post_markdown_content = table.Column<string>(type: "text", nullable: false),
                    post_author = table.Column<string>(type: "text", nullable: false),
                    PostKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.post_key);
                    table.ForeignKey(
                        name: "FK_posts_posts_PostKey",
                        column: x => x.PostKey,
                        principalSchema: "public",
                        principalTable: "posts",
                        principalColumn: "post_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "public",
                columns: table => new
                {
                    tag_key = table.Column<string>(type: "text", nullable: false),
                    tag_name = table.Column<string>(type: "text", nullable: false),
                    is_category = table.Column<bool>(type: "boolean", nullable: false),
                    PostKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.tag_key);
                    table.ForeignKey(
                        name: "FK_tags_posts_PostKey",
                        column: x => x.PostKey,
                        principalSchema: "public",
                        principalTable: "posts",
                        principalColumn: "post_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostKey",
                schema: "public",
                table: "posts",
                column: "PostKey");

            migrationBuilder.CreateIndex(
                name: "IX_tags_PostKey",
                schema: "public",
                table: "tags",
                column: "PostKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "posts",
                schema: "public");
        }
    }
}
