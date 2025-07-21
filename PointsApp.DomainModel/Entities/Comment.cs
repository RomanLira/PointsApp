namespace PointsApp.DomainModel.Entities;

/// <summary>
/// Комментарий
/// </summary>
public class Comment
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Точка, к которой относится комментарий
    /// </summary>
    public int PointId { get; set; }
    public virtual Point Point { get; set; }
    
    /// <summary>
    /// Цвет комментария
    /// </summary>
    public string? Color { get; set; }
    
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string? Text { get; set; }
}