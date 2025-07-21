using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PointsApp.DomainModel.Dtos;
using PointsApp.Infrastructure.Interfaces;

namespace PointsApp.Web.Controllers.Api;

[ApiController]
[Route("api/points")]
public class PointsController : ControllerBase
{
    private readonly IPointService _pointService;

    public PointsController(IPointService pointService)
    {
        _pointService = pointService;
    }

    /// <summary>
    /// Получает список всех точек
    /// </summary>
    /// <response code="200"> Успешный запрос. Возвращает список точек. </response>
    [HttpGet]
    [ProducesResponseType(typeof(List<PointDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var points = await _pointService.GetAll();
        return Ok(points);
    }
    
    /// <summary>
    /// Получает точку по её идентификатору
    /// </summary>
    /// <response code="200"> Успешный запрос. Возвращает точку. </response>
    /// <response code="404"> Точка не найдена. </response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PointDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var point = await _pointService.Get(id);

        if (point == null)
            return NotFound();

        return Ok(point);
    }

    /// <summary>
    /// Создаёт точку
    /// </summary>
    /// <response code="200"> Успешный запрос. Точка создана. </response>
    /// <response code="500"> Ошибка создания точки. </response>
    [HttpPost]
    [ProducesResponseType(typeof(PointDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] PointDto point)
    { 
        var result = await _pointService.Create(point);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет существующую точку
    /// </summary>
    /// <response code="200"> Успешный запрос. Точка обновлена. </response>
    /// <response code="404"> Точка не найдена. </response>
    /// <response code="500"> Ошибка обновления точки. </response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PointDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] PointDto point)
    {
        if (id != point.Id)
            return BadRequest();
        
        var result = await _pointService.Update(point);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Удаляет существующую точку
    /// </summary>
    /// <response code="200"> Успешный запрос. Точка удалена. </response>
    /// <response code="404"> Точка не найдена. </response>
    /// <response code="500"> Ошибка удаления точки. </response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    [ProducesResponseType( StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _pointService.Delete(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}