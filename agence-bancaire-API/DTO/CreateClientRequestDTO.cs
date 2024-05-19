namespace agence_bancaire_API.DTO
{
    public class CreateClientRequestDTO
    {
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
