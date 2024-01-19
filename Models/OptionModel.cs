using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoshanDemo1.Models
{
    
    public class OptionModel
    {
        [Key]
        public int OptionId { get; set; }
        public string OptionText { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public QuestionModel Question { get; set; }
    }
}
