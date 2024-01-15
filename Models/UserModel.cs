using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoshanDemo1.Models
{
    public class UserModel
    {
        
        public int? Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username {                                                                                                                                                                                                                                                                 get; set; }

        [Required(ErrorMessage = "Please select a role")]
        public string Gender { get; set; }

        public string Password { get; set; }

        public string InterestsString { get; set; }

        public string? Fruits { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }

        public string? ImagePath { get; set; }

        //public int? SelectedStateId { get; set; }
        //public int? SelectedCityId { get; set; }

        [NotMapped]
        public List<StateModel>? States { get; set; }

        [NotMapped]
        public List<CityModel>? Cities { get; set; }

    }
}
