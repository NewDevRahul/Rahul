using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;

namespace RoshanDemo1.Models
{
    public class QuestionModel
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public List<OptionModel> Options { get; set; }
    }
}
