using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using test.Models;

namespace RoshanDemo1.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        
        public DateTime? Date { get; set; } 
       
        public string Gender { get; set; }

        public string Password { get; set; }

        public string InterestsString { get; set; }
       
        public string Hobbies { get; set; } 

        public string Fruits { get; set; }

        [NotMapped]
        public IFormFile? ProfileImage { get; set; }

        public string? ImagePath { get; set; }

        public int? SelectedStateId { get; set; }
        public int? SelectedCityId { get; set; }
        public int? SelectedCountryId { get; set; }

        public string Address { get; set; }

        [NotMapped]
        public List<CountryModel>? Country { get; set; }

        [NotMapped]
        public List<StateModel>? States { get; set; }

        [NotMapped]
        public List<CityModel>? Cities { get; set; }

    }
}
