using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using AzureFunctions.Extensions.Swashbuckle.Attribute;

namespace SwaggerUIExample
{
    public static class GetFunctionQueryNPathParam
    {
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [QueryStringParameter("message", "message for user")]
        [QueryStringParameter("counter", "how many times?")]
        [FunctionName("GetFunctionQueryNPathParam")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetFunctionQueryNPathParam/{name}/{age}")] 
            HttpRequest req, string name, int age,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = req.Query["message"];
            int count = Convert.ToInt32(req.Query["count"]);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, My name is {name}, i am {age} years old.\nHere is my message: {message} for {count} times")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
