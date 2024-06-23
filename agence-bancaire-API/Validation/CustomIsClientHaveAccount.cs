using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class IsClientHaveAccountAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var ClientID = Convert.ToInt32(value.ToString());  

                if(clsAccount.isAccountExistByClientID(ClientID))
                {
                    return new ValidationResult(ErrorMessage = "this client already have an account");
                }else
                {
                    return ValidationResult.Success;
                }

            }
            
            return new ValidationResult("Invalid ClientID format.");
        }


    }
}
