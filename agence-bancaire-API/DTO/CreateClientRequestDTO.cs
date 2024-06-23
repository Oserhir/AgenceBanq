using agence_bancaire_API.Validation;

namespace agence_bancaire_API.DTO
{
    public class CreateClientRequestDTO
    {
        [CustomClientAlreadyExistByPersonID(ErrorMessage = "Client already exists for this person.!")]
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }

    }
}
