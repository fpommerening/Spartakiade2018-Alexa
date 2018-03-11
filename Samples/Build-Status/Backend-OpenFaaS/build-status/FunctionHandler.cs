using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
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

            if (requestType == typeof(IntentRequest))
            {
                response = ExecuteIntent(request);
            }
            else if (requestType == typeof(LaunchRequest))
            {
                response = ResponseBuilder.Tell("Herzlich willkommen");
            }
            else if (requestType == typeof(SessionEndedRequest))
            {
                response = ResponseBuilder.Empty();
            }

            Context.WriteContent(JsonConvert.SerializeObject(response, Formatting.Indented,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()}));
        }

        private static SkillResponse ExecuteIntent(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;
            if (intentRequest == null)
            {
               return ResponseBuilder.Tell("Fehler");
            }

            if (intentRequest.Intent.Name == "buildstatus")
            {
                return ResponseBuilder.Tell("Der Build für Projekt Spartakiade 2018 ist erfolgreich.");
            }

            return ResponseBuilder.Tell($"Intent {intentRequest.Intent.Name} ist unbekannt");
        }
    }
}
