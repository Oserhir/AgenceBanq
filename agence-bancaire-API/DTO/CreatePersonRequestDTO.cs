namespace agence_bancaire_API.DTO
{
    public class CreatePersonRequestDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CIN { get; set; }
    }
}
