using PointsApp.DomainModel.Dtos;

namespace PointsApp.Infrastructure.Interfaces;

public interface IPointService
{
    /// <summary>
    /// Возвращает все точки
    /// </summary>
    Task<List<PointDto>> GetAll();
    
    /// <summary>
    /// Возвращает точку по её идентификатору
    /// </summary>
    Task<PointDto?> Get(int id);
    
    /// <summary>
    /// Создает новую точку
    /// </summary>
    Task<PointDto> Create(PointDto entity);
    
    /// <summary>
    /// Обновляет точку
    /// </summary>
    Task<PointDto?> Update(PointDto entity);
    
    /// <summary>
    /// Удаляет точку
    /// </summary>
    Task<bool> Delete(int id);
}