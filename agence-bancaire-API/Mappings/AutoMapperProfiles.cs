using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using AutoMapper;

namespace agence_bancaire_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<clsClient, clsClientDTO>();
        }

    }
}
