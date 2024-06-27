using AgriWeatherTracker.Models;
using AutoMapper;

namespace AgriWeatherTracker.Mapper;
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
        CreateMap<WeatherDTO, Weather>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.Rainfall, opt => opt.MapFrom(src => src.Rainfall))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.WindSpeed))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new Location { Id = src.LocationId }));

        CreateMap<ConditionThreshold, ConditionThresholdDTO>();
        CreateMap<ConditionThresholdDTO, ConditionThreshold>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<HealthScore, HealthScoreDto>().ReverseMap();
    }
}


