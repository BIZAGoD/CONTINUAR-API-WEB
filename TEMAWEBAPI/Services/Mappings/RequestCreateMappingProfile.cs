using AutoMapper;

namespace TEMAWEBAPI;

public class RequestCreateMappingProfile : Profile
{
    public RequestCreateMappingProfile()
    {
        CreateMap<MangaCreateDTO, Manga>()
        .AfterMap(
            (src, dest) =>
            {
                dest.PublicationDate = DateTime.Now;
            }
        );
    }
}