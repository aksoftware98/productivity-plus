using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AKSoftware.ProductivityPlus.Server.Api
{
    public class WarmupFunction
    {
        [FunctionName("WarmupFunction")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Function run at: {DateTime.Now}");
        }
    }
}
