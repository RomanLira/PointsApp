using Microsoft.AspNetCore.Mvc;
using PointsApp.DomainModel.Dtos;
using PointsApp.Infrastructure.Interfaces;

namespace PointsApp.Web.Controllers.Api;

[ApiController]
[Route("api/comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    /// <summary>
    /// Получает комментарий по его идентификатору
    /// </summary>
    /// <response code="200"> Успешный запрос. Возвращает комментарий. </response>
    /// <response code="404"> Комментарий не найден. </response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var comment = await _commentService.Get(id);

        if (comment == null)
            return NotFound();

        return Ok(comment);
    }

    /// <summary>
    /// Создаёт комментарий
    /// </summary>
    /// <response code="200"> Успешный запрос. Комментарий создан. </response>
    /// <response code="500"> Ошибка создания комментария. </response>
    [HttpPost]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CommentDto comment)
    { 
        var result = await _commentService.Create(comment);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет существующий комментарий
    /// </summary>
    /// <response code="200"> Успешный запрос. Комментарий обновлен. </response>
    /// <response code="404"> Комментарий не найден. </response>
    /// <response code="500"> Ошибка обновления комментария. </response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CommentDto comment)
    {
        if (id != comment.Id)
            return BadRequest();
        
        var result = await _commentService.Update(comment);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Удаляет существующий комментарий
    /// </summary>
    /// <response code="204"> Успешный запрос. Комментарий удален. </response>
    /// <response code="404"> Комментарий не найден. </response>
    /// <response code="500"> Ошибка удаления комментария. </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _commentService.Delete(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}