using agence_bancaire_Business_Layer;

namespace agence_bancaire_API.Repositories
{
    public interface ItokenReposirtoy
    {
        string CreateJWTToken(clsUser User, string Role);
    }
}
