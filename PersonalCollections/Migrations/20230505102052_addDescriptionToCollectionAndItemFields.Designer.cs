﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalCollections.Data;

#nullable disable

namespace PersonalCollections.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230505102052_addDescriptionToCollectionAndItemFields")]
    partial class addDescriptionToCollectionAndItemFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonalCollections.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("PersonalCollections.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PersonalCollections.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CollectionId")
                        .HasColumnType("integer");

                    b.Property<int>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("CommentId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PersonalCollections.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PersonalCollections.Models.Tag_Item", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("ItemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("Tags_Items");
                });

            modelBuilder.Entity("PersonalCollections.Models.Item", b =>
                {
                    b.HasOne("PersonalCollections.Models.Collection", "Collection")
                        .WithMany("Items")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalCollections.Models.Comment", "Comments")
                        .WithMany("Items")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PersonalCollections.Models.Tag_Item", b =>
                {
                    b.HasOne("PersonalCollections.Models.Item", "Item")
                        .WithMany("Tags_Items")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalCollections.Models.Tag", "Tag")
                        .WithMany("Tags_Items")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PersonalCollections.Models.Collection", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PersonalCollections.Models.Comment", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PersonalCollections.Models.Item", b =>
                {
                    b.Navigation("Tags_Items");
                });

            modelBuilder.Entity("PersonalCollections.Models.Tag", b =>
                {
                    b.Navigation("Tags_Items");
                });
#pragma warning restore 612, 618
        }
    }
}
