using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Alexa
{
    public static class AlexaFunction
    {
        [FunctionName("Alexa-Scrum-Planner")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // Get request body
            SkillRequest request = await req.Content.ReadAsAsync<SkillRequest>();

            var requestType = request.GetRequestType();

            SkillResponse response = null;

            response = ResponseBuilder.Tell($"Request-Typ {requestType.Name}");

            return req.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(response, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));

        }
    }
}
