using System;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Function.Intents
{
    class ParticipantAddIntentHandler : BaseIntentHandler
    {
        public override string IntentName { get; } = "ParticipantAddIntent";

        protected override SkillResponse Handle(IntentRequest intentRequest, Session session, Context context)
        {

            if (intentRequest.Intent.Slots.TryGetValue("participant", out var Participant) && !string.IsNullOrEmpty(Participant.Value))
            {
                Console.WriteLine($"Erkannter Benutzer {Participant.Value}");

                return ResponseBuilder.Tell($"{Participant.Value} wurde hinzugefügt");
            }

            return ResponseBuilder.Tell("Es wurde kein Benutzer übergeben");
        }
    }
}
