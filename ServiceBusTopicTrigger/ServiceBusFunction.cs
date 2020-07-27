using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusTopicTrigger
{
    public class ServiceBusFunction
    {
        [FunctionName("TestFunction")]
        public static void Run([ServiceBusTrigger("supplierjobevents", "CSCP", Connection = "ServiceBusConnectionString")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
