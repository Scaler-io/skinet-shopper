using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Skinet.Shared.LoggerExtensions
{
    public static class LoggerBaseExtensions
    {
        public static ILogger Here(this ILogger logger, string method=null, string classname=null) 
        {

            if(!string.IsNullOrEmpty(method) && !string.IsNullOrEmpty(classname))
            {
                logger.LogInformation("Enterd to {method} of class {classname} \n", method, classname);
            }
            else
            {
                logger.LogInformation("Enterd\n");
            }
            return logger;
        }

        public static ILogger Exited(this ILogger logger, string method=null, string classname=null)
        {
            if (!string.IsNullOrEmpty(method) && !string.IsNullOrEmpty(classname))
            {
                logger.LogInformation("Exited to {method} of class {classname} \n", method, classname);
            }
            else
            {
                logger.LogInformation("Exited\n");
            }
            return logger;
        }

        public static ILogger WithId(this ILogger logger, object id)
        {
            logger.LogInformation($"With id {id}");
            return logger;
        }

        public static ILogger WithData(this ILogger logger, object data)
        {
            var deserializedObject = JsonSerializer.Serialize<object>(data);

            logger.LogInformation($"\nWith data {deserializedObject}");
            return logger;
        }
    }
}
