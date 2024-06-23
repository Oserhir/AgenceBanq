using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
