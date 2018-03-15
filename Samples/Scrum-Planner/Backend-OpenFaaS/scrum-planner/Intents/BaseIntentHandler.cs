using System;
using System.Net.Http;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Function.Intents
{
    public abstract class BaseIntentHandler
    {
        public abstract string IntentName { get; }

        public SkillResponse Handle(SkillRequest skillRequest)
        {
            var intentRequest = (IntentRequest)skillRequest.Request;
            return Handle(intentRequest, skillRequest.Session, skillRequest.Context);
        }

        protected abstract SkillResponse Handle(IntentRequest intentRequest, Session session, Context context);

        protected HttpClient GetServiceClient(Session session)
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"{session.User.UserId}:Spartakiade2018");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

        protected string ServiceUrl { get; } = "https://alexa.openfaas-dotnet.de/api/";
    }
}
