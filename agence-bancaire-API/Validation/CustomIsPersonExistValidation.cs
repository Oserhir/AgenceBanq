using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class IsPersonExistValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var personId = Convert.ToInt32(value.ToString());

                if (!clsPerson.isPersonExist(personId))
                {
                    return new ValidationResult($"The PersonID {personId} does not exist.");
                }else
                {
                    return ValidationResult.Success;
                }

            }else
            {
                return new ValidationResult("Invalid PersonID format.");
            }
            
        }

    }
}
