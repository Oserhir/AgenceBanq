using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class CustomDateOfBirthValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth > DateTime.UtcNow)
                {
                    return new ValidationResult("Date of Birth cannot be in the future");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
