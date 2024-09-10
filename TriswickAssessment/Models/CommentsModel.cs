using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class CommentsModel
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        //// Navigation property
        //public PostModel Post { get; set; }
    }
}
