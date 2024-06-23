using agence_bancaire_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
    public class CreatePersonRequestDTO
    {
        [Required(ErrorMessage = "firstName is not allowed to be empty")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "lastName is not allowed to be empty")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [CustomDateOfBirthValidation(ErrorMessage = "Date of Birth must be a valid date and should not be in the future")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "PhoneNumber is not allowed to be empty")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [MaxLength(50, ErrorMessage = "email length must be at least 12 characters long")]
        [UniqueEmail(ErrorMessage = "Sorry, that email address is already used!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is not allowed to be empty")]
        [MinLength(10, ErrorMessage = "Address must be at least 10 characters long")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CIN is not allowed to be empty")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CIN must be exactly 8 digits")]
        [UniqueCIN(ErrorMessage = "Sorry, that CIN is already used!")]
        public string CIN { get; set; }
    }
}
