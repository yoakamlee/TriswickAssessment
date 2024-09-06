using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }

        public string Tag { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual PostModel Post { get; set; }
    }
}
