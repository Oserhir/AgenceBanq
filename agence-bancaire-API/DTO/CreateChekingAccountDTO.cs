namespace agence_bancaire_API.DTO
{
    public class CreateChekingAccountDTO
    {
        public int Account_Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Balance { get; set; }
        public int overdraftLimit { get; set; }

    }
}
