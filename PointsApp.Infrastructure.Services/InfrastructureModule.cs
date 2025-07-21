using Microsoft.Extensions.DependencyInjection;
using PointsApp.Infrastructure.Interfaces;
using PointsApp.Utils.Modules;

namespace PointsApp.Infrastructure.Services;

public class InfrastructureModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddScoped<IPointService, PointService>();
        services.AddScoped<ICommentService, CommentService>();
    }
}