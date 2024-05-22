namespace agence_bancaire_API.DTO
{
    public class TransferDTO
    {
        public float Amount { get; set; }
        public int checkingaccount_id { get; set; }
        public int targetAccount_id { get; set; }

    }
}
