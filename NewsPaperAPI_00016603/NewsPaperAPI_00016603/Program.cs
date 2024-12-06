using Microsoft.EntityFrameworkCore;
using NewsPaperAPI_00016603.Data;
using NewsPaperAPI_00016603.MappingProfiles;
using NewsPaperAPI_00016603.Models;
using NewsPaperAPI_00016603.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GeneralDbContext>(o => o.UseSqlServer(
    builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NewsPaper API 16603",
        Version = "v1",
        Description = "WAD 00016603"
    });
});



builder.Services.AddTransient<IRepository<News>, NewsRepository>();
builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options =>
options.WithOrigins("http://http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
);

app.MapControllers();

app.Run();
