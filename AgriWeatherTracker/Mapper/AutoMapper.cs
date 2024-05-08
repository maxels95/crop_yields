using AgriWeatherTracker.Models;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CropDTO, Crop>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.GrowthCycles, opt => opt.MapFrom(src => 
                src.GrowthCycles.Select(id => new GrowthCycle { Id = id })))  // Correct mapping for GrowthCycles
            .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => 
                src.Locations.Select(id => new Location { Id = id })));

        CreateMap<Crop, CropDTO>()
            .ForMember(dto => dto.GrowthCycles, opt => opt.MapFrom(src => src.GrowthCycles))  // Maps GrowthCycles as usual
            .ForMember(dto => dto.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Id)));  // Maps only the Location IDs

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


