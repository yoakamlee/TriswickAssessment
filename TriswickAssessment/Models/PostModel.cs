using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriswickAssessment.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string OriginalPostId { get; set; }
        public string PostContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int LikeCount { get; set; }

        // Navigation properties
        public ICollection<CommentsModel> Comments { get; set; } = new List<CommentsModel>();
        public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();

    }

}
