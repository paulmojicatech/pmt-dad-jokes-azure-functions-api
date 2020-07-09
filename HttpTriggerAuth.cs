using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace PmtDadJokes
{

    public class AuthModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("pwd")]
        public string Pwd { get; set; }
    }

    public static class HttpTriggerAuth
    {
        [FunctionName("HttpTriggerAuth")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<AuthModel>(requestBody);
            var bytes = Convert.FromBase64String(data.Pwd);
            var encoded = ASCIIEncoding.ASCII.GetString(bytes);
            log.LogInformation($"Body of request: {data.UserId}");
            var isAuthenticated = encoded == Environment.GetEnvironmentVariable("Pwd");
            return new OkObjectResult(isAuthenticated);
        }
    }
}
