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
    }

}
