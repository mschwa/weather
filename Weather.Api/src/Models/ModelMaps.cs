using AutoMapper;

namespace Weather.Api.Models
{
    public class ModelMaps : Profile
    {
        public ModelMaps()
        {
            CreateMap<CityAndWeatherData, Climate>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Weather[0].Description))
                .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Coord.Lat))
                .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Coord.Lon))
                .ForMember(dest => dest.Lon, opt => opt.MapFrom(src => src.Coord.Lon))
                .ForMember(dest => dest.Temp, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.MaxTemp, opt => opt.MapFrom(src => src.Main.Temp_Max))
                .ForMember(dest => dest.MinTemp, opt => opt.MapFrom(src => src.Main.Temp_Min))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity));

            CreateMap<TimeZoneData, Climate>()
                .ForMember(dest => dest.TimeZoneName, opt => opt.MapFrom(src => src.TimeZoneName));

            CreateMap<ElevationData, Climate>()
                .ForMember(dest => dest.Elevation, opt => opt.MapFrom(src => src.Elevation));
        }
    }
}