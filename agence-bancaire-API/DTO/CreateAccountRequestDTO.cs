namespace agence_bancaire_API.DTO
{
    public class CreateAccountRequestDTO
    {
        public int ClientID { get; set; }
        public Guid AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
