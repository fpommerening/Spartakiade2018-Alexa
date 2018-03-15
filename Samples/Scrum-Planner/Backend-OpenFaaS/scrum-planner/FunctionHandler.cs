using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Function.Intents;
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

        private BaseIntentHandler[] handlers = 
        {
            new ParticipantAddIntentHandler(),
            new AppointmentCreateIntentHandler(), 
            new AppointmentListIntentHandler(), 

            new AmazonYesIntentHandler(), 
            new AmazonNoIntentHandler()
        };

        public override void Handle(string input)
        {
            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(input);
            SkillResponse response = null;

            var requestType = skillRequest.GetRequestType();

            if (requestType == typeof(IntentRequest))
            {
                response = HandleIntentRequest(skillRequest);
            }
            else
            {
                response = ResponseBuilder.Tell($"Request-Typ {requestType.Name}");
            }

            Context.WriteContent(JsonConvert.SerializeObject(response, Formatting.Indented,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()}));
        }

        private SkillResponse HandleIntentRequest(SkillRequest skillRequest)
        {
            var intentRequest = (IntentRequest)skillRequest.Request;
            foreach (var handler in handlers)
            {
                if (handler.IntentName == intentRequest.Intent.Name)
                {
                    return handler.Handle(skillRequest);
                }
            }
            return ResponseBuilder.Tell($"Der Aufruf {intentRequest.Intent.Name} ist unbekannt.");
        }
       
    }
}
