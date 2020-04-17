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
    public static class PostFunctionReqBody
    {
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(FunctionResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [FunctionName("PostFunctionReqBody")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "PostFunctionReqBody")]
            [RequestBodyType(typeof(UserData), "PostFunctionReqBody request")]
            HttpRequest req,
            [SwaggerIgnore] ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var userData = JsonConvert.DeserializeObject<UserData>(content);

            var functionResponse = new FunctionResponse
            {
                Name = userData.Name,
                BornAtYear = DateTime.Now.Year - userData.Age
            };
            return new OkObjectResult(functionResponse);
        }
    }

    public class UserData
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class FunctionResponse
    {
        public string Name { get; set; }
        public int BornAtYear { get; set; }
    }
}
