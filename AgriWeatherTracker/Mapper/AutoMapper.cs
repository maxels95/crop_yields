using AgriWeatherTracker.Models;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Crop, CropDTO>();
        CreateMap<GrowthCycleDTO, GrowthCycle>()
            .ForMember(dest => dest.Stages, opt => opt.MapFrom(src => src.Stages));

        // Map from GrowthCycle to GrowthCycleDTO
        CreateMap<GrowthCycle, GrowthCycleDTO>()
            .ForMember(dest => dest.Stages, opt => opt.MapFrom(src => src.Stages));

        CreateMap<GrowthStage, GrowthStageDTO>()
            .ForMember(dest => dest.OptimalConditions, opt => opt.MapFrom(src => src.OptimalConditions.Id))
            .ForMember(dest => dest.AdverseConditions, opt => opt.MapFrom(src => src.AdverseConditions.Id));
        CreateMap<GrowthStageDTO, GrowthStage>()
            .ForPath(dest => dest.OptimalConditions.Id, opt => opt.MapFrom(src => src.OptimalConditions))
            .ForPath(dest => dest.AdverseConditions.Id, opt => opt.MapFrom(src => src.AdverseConditions));

        CreateMap<Location, LocationDTO>();
        CreateMap<LocationDTO, Location>();

        CreateMap<Weather, WeatherDTO>()
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.Location.Id));

        CreateMap<ConditionThreshold, ConditionThresholdDTO>();
        CreateMap<ConditionThresholdDTO, ConditionThreshold>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}


