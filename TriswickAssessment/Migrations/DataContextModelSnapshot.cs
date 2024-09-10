﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TriswickAssessment.Data;

#nullable disable

namespace TriswickAssessment.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TriswickAssessment.Models.CommentsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("PostModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TriswickAssessment.Models.LikesModel", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeId"), 1L, 1);

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LikeId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("TriswickAssessment.Models.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("OriginalPostId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2024, 8, 31, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4894),
                            DateUpdated = new DateTime(2024, 8, 31, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4906),
                            LikeCount = 3,
                            OriginalPostId = "user1",
                            PostContent = "This is the first post."
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2024, 9, 5, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4908),
                            DateUpdated = new DateTime(2024, 9, 5, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4908),
                            LikeCount = 5,
                            OriginalPostId = "user2",
                            PostContent = "This is the second post."
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2024, 9, 8, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4909),
                            DateUpdated = new DateTime(2024, 9, 8, 13, 50, 9, 196, DateTimeKind.Local).AddTicks(4910),
                            LikeCount = 1,
                            OriginalPostId = "user3",
                            PostContent = "This is another interesting post."
                        });
                });

            modelBuilder.Entity("TriswickAssessment.Models.TagModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TriswickAssessment.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "user1",
                            Password = "Password123",
                            UserRole = "Regular",
                            Username = "regUser"
                        },
                        new
                        {
                            Id = "user2",
                            Password = "ModPassword123",
                            UserRole = "Moderator",
                            Username = "modUser"
                        });
                });

            modelBuilder.Entity("TriswickAssessment.Models.CommentsModel", b =>
                {
                    b.HasOne("TriswickAssessment.Models.PostModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostModelId");
                });

            modelBuilder.Entity("TriswickAssessment.Models.LikesModel", b =>
                {
                    b.HasOne("TriswickAssessment.Models.PostModel", "PostModel")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TriswickAssessment.Models.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostModel");

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("TriswickAssessment.Models.TagModel", b =>
                {
                    b.HasOne("TriswickAssessment.Models.PostModel", null)
                        .WithMany("Tags")
                        .HasForeignKey("PostModelId");
                });

            modelBuilder.Entity("TriswickAssessment.Models.PostModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
