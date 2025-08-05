using Microsoft.AspNetCore.Diagnostics;
using PokemonGame.Application.Exceptions;

namespace PokemonGame.API.ExceptionHandlers
{
    public class BabRequestExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BabRequestExceptionHandler> _logger;

        public BabRequestExceptionHandler(ILogger<BabRequestExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BadRequestException badRequestException)
            {
                return false;
            }
            _logger.LogError(badRequestException, "Bad request: {Message}", badRequestException.Message);
            return true;

        }
    }
}
