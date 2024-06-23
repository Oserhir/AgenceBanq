using agence_bancaire_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "personId is not allowed to be empty")]
        [CustomIsUserAlreadyExist()]
        [IsPersonExistValidation()]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "Password is not allowed to be empty")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
