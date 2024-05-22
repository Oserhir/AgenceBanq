using agence_bancaire_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
    public class CreatePersonRequestDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        [CustomDateOfBirthValidation(ErrorMessage = "Date of Birth must be a valid date and should not be in the future")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MinLength(10, ErrorMessage = "Address must be at least 10 characters long")]
        [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CIN is required")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CIN must be exactly 8 digits")]
        public string CIN { get; set; }
    }
}
