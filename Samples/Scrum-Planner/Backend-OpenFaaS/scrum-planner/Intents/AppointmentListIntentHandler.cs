using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Function.Models;
using Newtonsoft.Json;

namespace Function.Intents
{
    public class AppointmentListIntentHandler : BaseIntentHandler
    {
        public override string IntentName { get; } = "AppointmentListIntent";

        protected override SkillResponse Handle(IntentRequest intentRequest, Session session, Context context)
        {
            using (var client = GetServiceClient(session))
            {
                using (var response = client.GetAsync($"{ServiceUrl}appointment").Result)
                {
                    using (var content = response.Content)
                    {
                        var result = content.ReadAsStringAsync().Result;
                        var appointments = JsonConvert.DeserializeObject<List<Appointment>>(result);
                        var futureAppointments = appointments.Where(x => x.Date >= DateTime.Now.Date).ToArray();
                        StringBuilder sb = new StringBuilder();

                        if (!futureAppointments.Any())
                        {
                            return ResponseBuilder.Tell("Es sind keine Termine verfügbar");
                        }
                        sb.Append("<speak>");
                        if (futureAppointments.Length == 1)
                        {
                            sb.Append("Es ist ein Termin geplant.");
                        }
                        else
                        {
                            sb.Append($"Es sind {futureAppointments.Length} Termine geplant.");
                        }

                        sb.Append("<break strength=\"medium\"/>");
                        foreach (var appointment in futureAppointments.OrderBy(x=>x.Date))
                        {
                            sb.Append($"{appointment.Type} am ");
                            sb.Append($"<say-as interpret-as=\"date\">{appointment.Date:yyyy-MM-dd}</say-as>");
                            if (appointment.Participant == null || appointment.Participant.Length == 0)
                            {
                                sb.Append(" Es nimmt noch niemand teil");
                            }
                            else
                            {
                                sb.Append($" Es nehmen {string.Join(",", appointment.Participant)} teil.");
                            }

                            sb.Append("<break strength=\"medium\"/>");
                        }
                        sb.Append("</speak>");
                       return ResponseBuilder.Tell(new SsmlOutputSpeech {Ssml = sb.ToString()});

                    }
                }
            }
            return ResponseBuilder.Tell("Die Eingabe ist ungültig");
        }
    }
}
