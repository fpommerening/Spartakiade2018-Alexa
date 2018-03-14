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
    }
}
