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

        public override void Handle(string input)
        {
        }

       
    }
}
