using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriswickAssessment.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalPostId { get; set; }

        [Required]
        public string PostContent { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int LikeCount { get; set; }
        public List<TagModel> Tags { get; set; } = new List<TagModel>();

        public List<CommentsModel> Comments { get; set; } = new List<CommentsModel>();
    }

}
