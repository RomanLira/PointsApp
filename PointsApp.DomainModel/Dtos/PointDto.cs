using AutoMapper;
using PointsApp.DomainModel.Entities;
using PointsApp.DomainModel.Mappings;

namespace PointsApp.DomainModel.Dtos;

public class PointDto : IMapWith
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Координата Х
    /// </summary>
    public decimal X { get; set; }
    
    /// <summary>
    /// Координата Y
    /// </summary>
    public decimal Y { get; set; }
    
    /// <summary>
    /// Радиус точки
    /// </summary>
    public decimal Radius { get; set; }
    
    /// <summary>
    /// Цвет точки
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Список комментариев
    /// </summary>
    public virtual List<CommentDto> Comments { get; set; } = new();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Point, PointDto>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        
        profile.CreateMap<PointDto, Point>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
    }
}