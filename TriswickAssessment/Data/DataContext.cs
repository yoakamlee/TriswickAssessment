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
        public DbSet<PostModel> posts { get; set; }
        public DbSet<UserModel> users { get; set; }
        public DbSet<CommentsModel> comments { get; set; }
        public DbSet<LikesModel> likes { get; set; }
    }

}
