using System.Data.Entity;
using TriswickAssessment.Models;

namespace TriswickAssessment.Data
{
    public class DataContext : DbContext
        {
        public DataContext() : base("DefaultConnection")
            {

            }
        
        //DB tables to be created based on models - yls
        public DbSet<PostModel> posts {  get; set; }
        public DbSet<UserModel> users { get; set; }
        public DbSet<CommentsModel> comments { get; set; }
        public DbSet<LikesModel> likes { get; set; }
    }
   
}
