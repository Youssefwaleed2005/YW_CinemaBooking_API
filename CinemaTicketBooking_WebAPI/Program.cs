using Asp.Versioning;
using CinemaTicketBooking_WebAPI.Data;
using CinemaTicketBooking_WebAPI.Mapping;
using CinemaTicketBooking_WebAPI.Middleware;
using CinemaTicketBooking_WebAPI.Repos;
using CinemaTicketBooking_WebAPI.Repos.Interfaces;

using CinemaTicketBooking_WebAPI.Services;
using CinemaTicketBooking_WebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMoviesRepo, MoviesRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IAuditoriumRepo, AuditoriumRepo>();
builder.Services.AddScoped<IShowTimeRepo, ShowTimeRepo>();


builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAuditoriumService,AuditoriumService>();
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddAutoMapper(cfg =>
{ 
    cfg.AddProfile<MovieProfile>();
    cfg.AddProfile<BookingProfile>();
    cfg.AddProfile<CustomerProfile>();
    cfg.AddProfile<AuditoriumProfile>();
    cfg.AddProfile<ShowTimeProfile>();


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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
