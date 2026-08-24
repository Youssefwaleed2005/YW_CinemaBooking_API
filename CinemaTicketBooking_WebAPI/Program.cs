using Asp.Versioning;
using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Mapping;
using CinemaTicketBooking_WebAPI.Middleware;
using CinemaTicketBooking_WebAPI.Repos;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;

using CinemaTicketBooking_WebAPI.Services;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMoviesRepo, MoviesRepo>();
builder.Services.AddScoped<IMovieService, MovieService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(cfg =>
{ 
cfg.AddProfile<MovieProfile>();


});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();   
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
