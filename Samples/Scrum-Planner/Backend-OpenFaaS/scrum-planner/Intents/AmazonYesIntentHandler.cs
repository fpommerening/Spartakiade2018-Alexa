using System;
using System.Net.Http;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Newtonsoft.Json;

namespace Function.Intents
{
    public class AmazonYesIntentHandler : BaseIntentHandler
    {
        public override string IntentName { get; } = "AMAZON.YesIntent";

        protected override SkillResponse Handle(IntentRequest intentRequest, Session session, Context context)
        {
            if (session.Attributes.TryGetValue("state", out var stateValue))
            {
                var state = (IntentState) Convert.ToInt32(stateValue);
                switch (state)
                {
                    case IntentState.AppointmentCreate:
                        return CreateAppointment(intentRequest, session, context);
                    default:
                        return ResponseBuilder.Tell($"Der Status {state} ist unbekannt");
                }
            }
            else
            {
                return ResponseBuilder.Tell("Es wurde kein Funktion gewählt. Du kannst einen neue Termin anlegen oder Termine abfragen");
            }
        }

        private SkillResponse CreateAppointment(IntentRequest intentRequest, Session session, Context context)
        {
            DateTime date = DateTime.Now;
            string type = string.Empty;
            if (session.Attributes.TryGetValue("date", out var dateValue))
            {
                date = Convert.ToDateTime(dateValue);
            }
            if (session.Attributes.TryGetValue("type", out var typeValue))
            {
                type = typeValue.ToString();

            }

            var appointment = new
            {
                date = date,
                type = type
            };
            var json = JsonConvert.SerializeObject(appointment);

            using (var client = GetServiceClient(session))
            {
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = client.PutAsync($"{ServiceUrl}appointment", stringContent).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return ResponseBuilder.Tell("Der Termin wurde erstellt du kannst nun Teilnehmer ergänzen.");
                    }
                }
            }
            return ResponseBuilder.Tell("Jawohll ja");
        }
    }
}
