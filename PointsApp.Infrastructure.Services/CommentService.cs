using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointsApp.DataAccess.EF;
using PointsApp.DomainModel.Dtos;
using PointsApp.DomainModel.Entities;
using PointsApp.Infrastructure.Interfaces;

namespace PointsApp.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CommentService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CommentDto?> Get(int id)
    {
        var entity = await _dbContext.Comments
            .FirstOrDefaultAsync(_ => _.Id == id);

        if (entity == null)
            return null;

        return _mapper.Map<CommentDto>(entity);
    }

    public async Task<CommentDto> Create(CommentDto entity)
    {
        var comment = _mapper.Map<Comment>(entity);

        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CommentDto>(comment);
    }
    
    public async Task<CommentDto?> Update(CommentDto entity)
    {
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(p => p.Id == entity.Id);

        if (comment == null)
            return null;
        
        comment.PointId = entity.PointId;
        comment.Text = entity.Text;
        comment.Color = entity.Color;

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _dbContext.Comments
            .FirstOrDefaultAsync(_ => _.Id == id);

        if (entity == null)
            return false;

        _dbContext.Comments.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}