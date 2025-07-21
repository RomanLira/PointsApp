using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PointsApp.DataAccess.EF;
using PointsApp.DomainModel.Mappings;
using PointsApp.Infrastructure.Services;
using PointsApp.Utils.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("PointsDb");
    options.UseLazyLoadingProxies();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.RegisterModule<InfrastructureModule>(builder.Configuration);

builder.Services.AddAutoMapper(configuration => configuration.AddProfile(new AssemblyMappingProfile()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "api";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();