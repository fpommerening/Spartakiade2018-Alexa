using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenFaaS.Dotnet;

namespace Function
{
    public class FunctionHandler : BaseFunction
    {
        public FunctionHandler(IFunctionContext functionContext)
            : base(functionContext)
        {
        }

        public override void Handle(string input)
        {
            var request = JsonConvert.DeserializeObject<SkillRequest>(input);

            var requestType = request.GetRequestType();

            SkillResponse response = null;

            response = ResponseBuilder.Tell($"Request-Typ {requestType.Name}");

            Context.WriteContent(JsonConvert.SerializeObject(response, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }
}
