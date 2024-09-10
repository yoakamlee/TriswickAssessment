using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class TagModel
    {
        [Key]
        public int Id { get; set; }

        public string Tag { get; set; }


        //[ForeignKey("PostId")]
        //public virtual PostModel Post { get; set; }
    }
}
