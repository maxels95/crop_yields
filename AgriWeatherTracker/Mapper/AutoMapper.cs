using AgriWeatherTracker.Models;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Crop, CropDTO>();
        CreateMap<GrowthCycle, GrowthCycleDTO>();
        CreateMap<GrowthStage, GrowthStageDTO>();
        CreateMap<Location, LocationDTO>();
        CreateMap<LocationDTO, Location>();
        CreateMap<Weather, WeatherDTO>()
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Location.Id));
        CreateMap<ConditionThreshold, ConditionThresholdDTO>();
    }
}


