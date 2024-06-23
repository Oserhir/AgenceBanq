using agence_bancaire_API.Validation;
using System.ComponentModel.DataAnnotations;

namespace agence_bancaire_API.DTO
{
   // [CustomIsClientAlreadyExist]
   // [IsClientHaveAccount()]
    public class CreateAccountRequestDTO
    {
        public int ClientID { get; set; }
        public float? Balance { get; set; }
        public int? overdraftLimit { get; set; }
    }
}
