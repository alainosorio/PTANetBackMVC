namespace Retailer.Setup;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client.Model.Retail, Storage.Sqlserver.Retail>();
        CreateMap<Client.Model.Retail, Storage.Sqlserver.Retail>().ReverseMap();
    }
}
