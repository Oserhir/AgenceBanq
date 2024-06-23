using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace agence_bancaire_API.Validation
{
    public class CustomIsClientAlreadyExistAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult("Client ID is required.");
            }

            if (int.TryParse(value.ToString(), out int clientId))
            {
                if (!clsClient.isClientExist(clientId))
                {
                    return new ValidationResult($"The clientId {clientId} does not exist.");
                }

                // Further validation logic here...

                return ValidationResult.Success;
            }

            return new ValidationResult("Client ID must be an integer.");

        }
    }
}
