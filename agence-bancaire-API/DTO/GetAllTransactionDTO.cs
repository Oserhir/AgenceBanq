namespace agence_bancaire_API.DTO
{
    public class GetAllTransactionDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public String Type { get; set; }
        public int idOperation { get; set; }
        public String Operation { get; set; }
        public int? targetAccount_id { get; set; }


        public GetAllTransactionDTO(int TransactionId, int AccountId, float Amount, DateTime TransactionDate
          , String Type, int idOperation, String opeartionID, int? targetAccount_id)
        {
            this.TransactionId = TransactionId;
            this.AccountId = AccountId;
            this.Amount = Amount;
            this.TransactionDate = TransactionDate;
            this.Type = Type;
            this.idOperation = idOperation;
            this.Operation = opeartionID;
            this.targetAccount_id = targetAccount_id;
        }
    }
}
