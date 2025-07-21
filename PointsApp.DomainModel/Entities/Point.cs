namespace PointsApp.DomainModel.Entities;

/// <summary>
/// Точка
/// </summary>
public class Point
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
    public virtual List<Comment> Comments { get; set; } = new();
}