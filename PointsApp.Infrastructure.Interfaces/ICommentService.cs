using PointsApp.DomainModel.Dtos;

namespace PointsApp.Infrastructure.Interfaces;

public interface ICommentService
{
    /// <summary>
    /// Возвращает комментарий по его идентификатору
    /// </summary>
    Task<CommentDto?> Get(int id);
    
    /// <summary>
    /// Создает новый комментарий
    /// </summary>
    Task<CommentDto> Create(CommentDto entity);
    
    /// <summary>
    /// Обновляет комментарий
    /// </summary>
    Task<CommentDto?> Update(CommentDto entity);
    
    /// <summary>
    /// Удаляет комментарий
    /// </summary>
    Task<bool> Delete(int id);
}