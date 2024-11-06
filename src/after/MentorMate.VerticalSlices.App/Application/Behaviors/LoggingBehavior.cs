using MediatR;

namespace MentorMate.VerticalSlices.App.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using (logger.BeginScope(request))
        {
            logger.LogInformation("Executing handler");

            var result = await next();
        
            logger.LogInformation("Executed handler with Response: {@Response}", result);

            // Return response
            return result;    
        }
    }
}