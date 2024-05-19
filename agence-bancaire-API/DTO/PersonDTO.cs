namespace agence_bancaire_API.DTO
{
    public class PersonDTO
    {
        public int PersonID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CIN { get; set; }

        public PersonDTO(int PersonID, string firstName, string lastName, DateTime DateOfBirth, string PhoneNumber, string Email, string Address
            , string CIN)
        {
            this.PersonID = PersonID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.DateOfBirth = DateOfBirth;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.CIN = CIN;
        }

    }
}
