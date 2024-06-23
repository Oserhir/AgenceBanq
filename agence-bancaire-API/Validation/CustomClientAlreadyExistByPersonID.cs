using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class CustomClientAlreadyExistByPersonIDAttribute: ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var PersonID = Convert.ToInt32(value);

                if (clsClient.isClientExistByPersonIDAsync(PersonID))
                {
                    return new ValidationResult("Person already has a client.");
                }
            }

            return ValidationResult.Success;
        }

    }
}
