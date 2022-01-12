using Microsoft.Extensions.Logging;

namespace Skinet.Shared.LoggerExtensions
{
    public static class LoggerOrderingExtensions
    {
        public static ILogger WithBasketId(this ILogger logger, object id){
            logger.LogInformation($"With basket id {id}\n");
            return logger;
        }
        public static ILogger WithOrderId(this ILogger logger, object id){
            logger.LogInformation($"With order id {id}\n");
            return logger;
        }
        public static ILogger DeliverMethodOpted(this ILogger logger, object method){
            logger.LogInformation($"Customer opted delivery method {method}\n");
            return logger;
        }
    }
}