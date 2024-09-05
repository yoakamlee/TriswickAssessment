using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class CommentsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string OriginalPostId { get; set; }

        [Required]
        public string PostContent { get; set; }
        public DateTime CommentDate { get; set; }

        [ForeignKey("PostId")]
        public virtual PostModel PostModel { get; set; }
    }
}
