using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace Function.Intents
{
    public class AmazonNoIntentHandler : BaseIntentHandler
    {
        public override string IntentName { get; } = "AMAZON.NoIntent";

        protected override SkillResponse Handle(IntentRequest intentRequest, Session session, Context context)
        {
            


            return ResponseBuilder.Tell("Der Benutzer hat nein gesagt");
        }
    }
}
