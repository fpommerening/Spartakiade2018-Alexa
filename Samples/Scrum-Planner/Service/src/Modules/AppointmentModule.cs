using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Service.Modules
{
    public class AppointmentModule : Nancy.NancyModule
    {
        public AppointmentModule()
        {
            this.RequiresAuthentication();

            Get("/api/appointment", action: async (args, ct) =>
            {
                var dl = new Data.DataLayer();
                var dbo = await dl.GetAppointments(Context.CurrentUser.Identity.Name);
                var dto = dbo.Select(MapToDto).ToArray();
                return Response.AsJson(dto);
            });

            Put("/api/appointment", action: async (args, ct) =>
            {
                var dl = new Data.DataLayer();
                var dto = this.Bind<Models.Appointment>();

                var dbo = new Data.Appointment
                {
                    ExternalId = Guid.NewGuid(),
                    CreateOn = DateTime.UtcNow,
                    Type = Data.AppointmentType.Planning,
                    Date = dto.Date,
                    UserId = Context.CurrentUser.Identity.Name
                };
                dbo.Type = Enum.Parse<Data.AppointmentType>(dto.Type);

                await dl.SaveAppointment(dbo);


                return HttpStatusCode.Created;
            });

            Post("/api/appointment/{id}", action: async (args, ct) =>
            {
                var dl = new Data.DataLayer();
                var id = Guid.Parse(args.id);
                var dto = this.Bind<Models.Appointment>();
                Data.Appointment dbo = await dl.GetAppointment(id, Context.CurrentUser.Identity.Name);

                dbo.Date = dto.Date;
                dbo.Type = Enum.Parse<Data.AppointmentType>(dto.Type);

                await dl.SaveAppointment(dbo);

                return HttpStatusCode.OK;
            });


            Put("/api/appointment/{id}/participant/{name}", action: async (args, ct) =>
            {
                var id = Guid.Parse(args.id);
                var name = args.name;

                var dl = new Data.DataLayer();
                Data.Appointment dbo = await dl.GetAppointment(id, Context.CurrentUser.Identity.Name);
                if (dbo.Participants == null)
                {
                    dbo.Participants = new List<Data.Participant>();
                }
                var participant = dbo.Participants.FirstOrDefault(x => x.Name == name);
                if (participant == null)
                {
                    participant = new Data.Participant
                    {
                        Name = name,
                        CreatedAt = DateTime.UtcNow
                    };
                    dbo.Participants.Add(participant);
                }
                participant.DeletedAt = null;

                await dl.SaveAppointment(dbo);

                return HttpStatusCode.Created;
            });

            Delete("/api/appointment/{id}/participant/{name}", action: async (args, ct) =>
            {
                var dl = new Data.DataLayer();

                var id = Guid.Parse(args.id);
                var name = args.name;

                Data.Appointment dbo = await dl.GetAppointment(id, Context.CurrentUser.Identity.Name);
                if (dbo.Participants == null)
                {
                    dbo.Participants = new List<Data.Participant>();
                }
                var participant = dbo.Participants.FirstOrDefault(x => x.Name == name);
                if (participant != null)
                {
                    participant.DeletedAt= DateTime.UtcNow;
                }
                await dl.SaveAppointment(dbo);
                return HttpStatusCode.OK;
            });
        }

        private static Models.Appointment MapToDto(Data.Appointment dbo)
        {
            var dto = new Models.Appointment
            {
                Date = dbo.Date,
                CreateOn = dbo.CreateOn,
                Id = dbo.ExternalId
            };
            if (dbo.Participants != null)
            {
                dto.Participant = dbo.Participants.Where(x => !x.DeletedAt.HasValue).Select(x => x.Name).ToArray();
            }

            dto.Type = dbo.Type.ToString();
            return dto;
        }

    }
    

    
}
