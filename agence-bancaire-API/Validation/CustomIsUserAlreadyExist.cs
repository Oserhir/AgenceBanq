using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class CustomIsUserAlreadyExistAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value != null)
            {
                var personId = Convert.ToInt32(value.ToString());   

                if( clsUser.IsUserExisForPersonID(personId) )
                {
                    return new ValidationResult(ErrorMessage = $"A user already exists for PersonID {personId}.");
                }else
                {
                    return ValidationResult.Success;
                }

            }

            return new ValidationResult("Invalid PersonID format.");
        }


    }
}
