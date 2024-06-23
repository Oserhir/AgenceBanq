using agence_bancaire_Business_Layer;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.Validation
{
    public class UniqueCINAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           if(value != null)
           {
                var CIN = value.ToString();

                if ( clsPerson.IsCINExist(CIN) )
                {
                    return new ValidationResult("CIN Already Exist");
                }

           }

           return ValidationResult.Success;
        }

    }
}
