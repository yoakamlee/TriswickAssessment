using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string Tag { get; set; }

        // Foreign Key to the Post
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual PostModel Post { get; set; }
    }
}
