namespace agence_bancaire_API.DTO
{
    public class CheckingAccountDTO
    {
        public int Account_Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Balance { get; set; }
        public float overdraftLimit { get; set; }


        public CheckingAccountDTO(int Account_Id, DateTime CreatedDate, float Balance, float overdraftLimit)
        {
            this.Account_Id = Account_Id;
            this.CreatedDate = CreatedDate;
            this.Balance = Balance;
            this.overdraftLimit = overdraftLimit;
        }


    }
    

    

}
