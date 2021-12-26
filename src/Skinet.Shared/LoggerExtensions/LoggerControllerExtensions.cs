using Microsoft.Extensions.Logging;


namespace Skinet.Shared.LoggerExtensions
{
    public static class LoggerControllerExtensions
    {
        public static ILogger Controller(this ILogger logger, string controllerName, string actionName)
        {
            logger.LogInformation($"Type: controller\nName: {controllerName}\nAction: {actionName}\n");
            return logger;
        }

        public static ILogger HttpGet(this ILogger logger)
        {
            logger.LogInformation("HttpMethod: Get\n");
            return logger;
        }
        public static ILogger HttpPost(this ILogger logger)
        {
            logger.LogInformation("HttpMethod: Post\n");
            return logger;
        }
        public static ILogger HttpPut(this ILogger logger)
        {
            logger.LogInformation("HttpMethod: Put\n");
            return logger;
        }
        public static ILogger HttpDelete(this ILogger logger)
        {
            logger.LogInformation("HttpMethod: Delete\n");
            return logger;
        }

    }
}
