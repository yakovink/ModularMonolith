using System;


namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> logger):
     IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull, IRequest<TResponse>
     where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "[START] Handle request = {request} - Response = {response} - RequestData={RequestData}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request
        );

        var timer = Stopwatch.StartNew();
        timer.Start();

        var response = await next();
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogInformation(
                "[Performence] The request {request} took {timeTaken} seconds",
                typeof(TRequest).Name,
                timeTaken
            );
        }
        logger.LogInformation(
            "[END] Handled {request} with {response} at {timeTaken}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            timeTaken
        );
        return response;
    }
}
