﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VovaLantsovBlog.Data;

namespace VovaLantsovBlog.Data.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210328183646_ManyToManyPostsTags")]
    partial class ManyToManyPostsTags
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<string>("PostsKey")
                        .HasColumnType("text");

                    b.Property<string>("TagsKey")
                        .HasColumnType("text");

                    b.HasKey("PostsKey", "TagsKey");

                    b.HasIndex("TagsKey");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("VovaLantsovBlog.Shared.Models.Post", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("post_key");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("post_author");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("post_image_url");

                    b.Property<DateTime>("LastEditedTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("post_last_edited");

                    b.Property<string>("MarkdownContent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("post_markdown_content");

                    b.Property<string>("PostKey")
                        .HasColumnType("text");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("post_title");

                    b.HasKey("Key");

                    b.HasIndex("PostKey");

                    b.ToTable("posts", "public");
                });

            modelBuilder.Entity("VovaLantsovBlog.Shared.Models.Tag", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("tag_key");

                    b.Property<bool>("IsCategory")
                        .HasColumnType("boolean")
                        .HasColumnName("is_category");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tag_name");

                    b.HasKey("Key");

                    b.ToTable("tags", "public");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("VovaLantsovBlog.Shared.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VovaLantsovBlog.Shared.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VovaLantsovBlog.Shared.Models.Post", b =>
                {
                    b.HasOne("VovaLantsovBlog.Shared.Models.Post", null)
                        .WithMany("ReadMorePosts")
                        .HasForeignKey("PostKey");
                });

            modelBuilder.Entity("VovaLantsovBlog.Shared.Models.Post", b =>
                {
                    b.Navigation("ReadMorePosts");
                });
#pragma warning restore 612, 618
        }
    }
}