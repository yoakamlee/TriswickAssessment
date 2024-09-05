using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class LikesModel
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("PostId")]
        public virtual PostModel PostModel { get; set; }

        [ForeignKey("UserId")]
        public virtual UserModel UserModel { get; set; }
    }
}
