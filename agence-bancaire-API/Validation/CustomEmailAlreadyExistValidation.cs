using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();

                if(clsPerson.IsEmailExist(email))
                {
                    return new ValidationResult("Email already exists.");
                }

            }

            return ValidationResult.Success;
        }


    }
}
