using AutoMapper;
using PointsApp.DomainModel.Entities;
using PointsApp.DomainModel.Mappings;

namespace PointsApp.DomainModel.Dtos;

public class CommentDto : IMapWith
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Точка, к которой относится комментарий
    /// </summary>
    public int PointId { get; set; }
    
    /// <summary>
    /// Цвет комментария
    /// </summary>
    public string? Color { get; set; }
    
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string? Text { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, CommentDto>()
            .ReverseMap();
    }
}