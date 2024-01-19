using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RoshanDemo1.Models
{
    [Keyless]
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        //public string SelectedOptionIds { get; set; }

        public List<int> SelectedOptionIds { get; set; }
    }
}
