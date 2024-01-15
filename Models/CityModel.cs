using System.ComponentModel.DataAnnotations;

namespace RoshanDemo1.Models
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
