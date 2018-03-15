using System;
using System.Collections.Generic;
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
                var responseSession = new Session
                {
                    Attributes = new Dictionary<string, object>
                    {
                        {"participant", Participant.Value},
                        {"state", IntentState.ParticipantAdd }
                    }
                };
                var anwser = new PlainTextOutputSpeech {Text = $"Soll {Participant.Value} hinzugefügt werden?"};
                var repompt = new Reprompt
                {
                    OutputSpeech = new PlainTextOutputSpeech {Text = $"{Participant.Value} hinzufügen"}
                };
                return ResponseBuilder.Ask(anwser, repompt, responseSession);
            }

            return ResponseBuilder.Tell("Es wurde kein Benutzer übergeben");
        }
    }
}
