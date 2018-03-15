using System.Collections.Generic;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Function.Intents
{
    public class AppointmentCreateIntentHandler : BaseIntentHandler
    {
        public override string IntentName { get; } = "AppointmentCreateIntent";

        protected override SkillResponse Handle(IntentRequest intentRequest, Session session, Context context)
        {
            string date = string.Empty;
            string type = string.Empty;
            if (intentRequest.Intent.Slots.TryGetValue("Date", out var dateSlot))
            {
                date = dateSlot.Value;
            }
            if (intentRequest.Intent.Slots.TryGetValue("Type", out var typeSlot))
            {
                type = typeSlot.Resolution.Authorities[0].Values[0].Value.Id;
            }

            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(type))
            {
                var responseSession = new Session
                {
                    Attributes = new Dictionary<string, object>
                    {
                        {"date", date},
                        {"state", IntentState.AppointmentCreate },
                        {"type", type }
                    }
                };
                var anwser = new SsmlOutputSpeech
                {
                    Ssml = $"<speak>Soll das {type} am<say-as interpret-as=\"date\">{date}</say-as> erstellt werden?</speak>"
                };

                var repompt = new Reprompt{OutputSpeech = new SsmlOutputSpeech
                {
                    Ssml = $"<speak>{type}<say-as interpret-as=\"date\">{date}</say-as> erstellen?</speak>"
                }};
                return ResponseBuilder.Ask(anwser, repompt, responseSession);
            }

            return ResponseBuilder.Tell("Die Eingabe ist ungültig");
        }
    }
}
