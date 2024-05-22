namespace agence_bancaire_API.DTO
{
    public class clsUserDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CIN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }

        public clsUserDTO(string firstName, string lastName, string PhoneNumber, 
            string Address, string Email, string CIN, DateTime DateOfBirth
            , string Password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.Email = Email;
            this.CIN = CIN;
            this.DateOfBirth = DateOfBirth;
            this.Password = Password;
        }

    }
}
