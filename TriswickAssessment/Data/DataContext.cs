using Microsoft.EntityFrameworkCore;
using TriswickAssessment.Models;

namespace TriswickAssessment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
              : base(options)
        {

        }

        //DB tables to be created based on mo
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CommentsModel> Comments { get; set; }
        public DbSet<LikesModel> Likes { get; set; }
        public DbSet<TagModel> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostModel>().HasData(
                new PostModel
                {
                    Id = 1,
                    OriginalPostId = "user1",
                    PostContent = "This is the first post.",
                    DateCreated = DateTime.Now.AddDays(-10),
                    DateUpdated = DateTime.Now.AddDays(-10),
                    LikeCount = 3
                },
                new PostModel
                {
                    Id = 2,
                    OriginalPostId = "user2",
                    PostContent = "This is the second post.",
                    DateCreated = DateTime.Now.AddDays(-5),
                    DateUpdated = DateTime.Now.AddDays(-5),
                    LikeCount = 5
                },
                new PostModel
                {
                    Id = 3,
                    OriginalPostId = "user3",
                    PostContent = "This is another interesting post.",
                    DateCreated = DateTime.Now.AddDays(-2),
                    DateUpdated = DateTime.Now.AddDays(-2),
                    LikeCount = 1
                }
            );
        }
    }

}
