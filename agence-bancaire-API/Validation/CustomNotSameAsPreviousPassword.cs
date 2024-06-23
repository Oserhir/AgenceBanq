using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class CustomNotSameAsPreviousPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        if (value != null)
        {
            var Password = value.ToString();
                // 

           if (Password == "" )
            {
                return new ValidationResult(ErrorMessage = $"Old password and new password must be different.  Please try again");
            }
            else
            {
                return ValidationResult.Success;
            }

        }

        return new ValidationResult("Invalid PersonID format.");
    }


}
}
