using LearningManagementSystem.Application.Features.Courses.Commands;
using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Infrastructure.Data;
using LearningManagementSystem.Infrastructure.Repositories;
using LearningManagementSystem.Infrastructure.Settings;
using LearningManagementSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IDbContext, MongoDbContext>();
builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCourseCommand).Assembly));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

await DbInitializer.InitializeAsync(app.Services.GetRequiredService<IDbContext>());

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
