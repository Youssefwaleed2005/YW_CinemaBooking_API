using CinemaTicketBooking_WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CinemaTicketBooking_WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (MovieNotFoundException ex)
            {
                await WriteProblem(context, 404, "Movie Not Found", ex.Message);
            }
            catch (BookingNotFoundException ex)
            {
                await WriteProblem(context, 404, "Booking Not Found", ex.Message);
            }
            catch (ShowTimeNotFoundException ex)
            {
                await WriteProblem(context, 404, "ShowTime Not Found", ex.Message);
            }
            catch (CustomerNotFoundException ex)
            {
                await WriteProblem(context, 404, "Customer Not Found", ex.Message);
            }
          
            catch (MovieAlreadyExistsException ex)
            {
                await WriteProblem(context, 409, "Movie Already Exists", ex.Message);
            }
            
            catch (InvalidBookingException ex)
            {
                await WriteProblem(context, 400, "Invalid Booking", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteProblem(context, 500, "Internal Server Error", "An unexpected error occurred.");
            }
        }

        private async Task WriteProblem(HttpContext context, int statusCode, string title, string detail)
        {
            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}