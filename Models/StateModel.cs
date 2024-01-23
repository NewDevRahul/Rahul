using System.ComponentModel.DataAnnotations;

namespace RoshanDemo1.Models
{
    public class StateModel
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
