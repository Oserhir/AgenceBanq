namespace agence_bancaire_API.Global_Classes
{
    public class util
    {
        public static Guid GenerateGUID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid;
        }




    }
}
