using agence_bancaire_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
    public class ChangeUserPassword
    {
        [Required(ErrorMessage = "Password is required")]
        // FluentValidation
        public string Password { get; set; }

    }
}
