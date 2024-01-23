using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class CountryModel
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }

    }
}
