using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PointsApp.DataAccess.EF;
using PointsApp.DomainModel.Dtos;
using PointsApp.DomainModel.Entities;
using PointsApp.Infrastructure.Interfaces;

namespace PointsApp.Infrastructure.Services;

public class PointService : IPointService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PointService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<PointDto>> GetAll()
    {
        var query = _dbContext.Points;

        var points = await query
            .OrderBy(_ => _.Id)
            .ProjectTo<PointDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return points;
    }

    public async Task<PointDto?> Get(int id)
    {
        var entity = await _dbContext.Points
            .FirstOrDefaultAsync(_ => _.Id == id);

        if (entity == null)
            return null;

        return _mapper.Map<PointDto>(entity);
    }

    public async Task<PointDto> Create(PointDto entity)
    {
        var point = _mapper.Map<Point>(entity);

        await _dbContext.Points.AddAsync(point);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<PointDto>(point);
    }
    
    public async Task<PointDto?> Update(PointDto entity)
    {
        var point = await _dbContext.Points
            .FirstOrDefaultAsync(p => p.Id == entity.Id);

        if (point == null)
            return null;

        point.X = entity.X;
        point.Y = entity.Y;
        point.Radius = entity.Radius;
        point.Color = entity.Color;

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<PointDto>(point);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _dbContext.Points
            .FirstOrDefaultAsync(_ => _.Id == id);

        if (entity == null)
            return false;

        _dbContext.Points.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DeleteAll()
    {
        var entities = await _dbContext.Points.ToListAsync();
        
        _dbContext.Points.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}