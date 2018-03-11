using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Service.Data
{
    public class DataLayer
    {
        private IMongoClient _mongoClient;

        public async Task<string> SaveAppointment(Appointment appointment)
        {
            var client = Client();
            var db = client.GetDatabase("ScrumPlannerDataStore");
            var collection = db.GetCollection<Appointment>("Appointments");
            if (appointment.Id == ObjectId.Empty)
            {
                await collection.InsertOneAsync(appointment);
            }
            else
            {
                var filter = Builders<Appointment>.Filter.Eq(s => s.Id, appointment.Id);
                await collection.ReplaceOneAsync(filter, appointment);
            }
            
            return appointment.Id.ToString();
        }

        public async Task<Appointment> GetAppointment(Guid id, string userId)
        {
            var client = Client();
            var db = client.GetDatabase("ScrumPlannerDataStore");
            var collection = db.GetCollection<Appointment>("Appointments");
            var builder = Builders<Appointment>.Filter;
            var filter = builder.And(new FilterDefinition<Appointment>[]
            {
                builder.Eq(x => x.UserId, userId),
                builder.Eq(x => x.ExternalId, id)
            });

            using (var filterResult = await collection.FindAsync(filter))
            {
                return await filterResult.FirstOrDefaultAsync();
            }
        }

        public async Task<List<Appointment>> GetAppointments(string userId)
        {
            var client = Client();
            var db = client.GetDatabase("ScrumPlannerDataStore");
            var collection = db.GetCollection<Appointment>("Appointments");

            var builder = Builders<Appointment>.Filter;
            var filter = builder.Eq(x => x.UserId, userId);
            var resultList = new List<Appointment>();
            using (var filterResult = await collection.FindAsync(filter))
            {
                while (await filterResult.MoveNextAsync())
                {
                    resultList.AddRange(filterResult.Current);
                }
            }
            return resultList;
        }

        private IMongoClient Client()
        {
            return _mongoClient ?? (_mongoClient = new MongoClient("mongodb://localhost"));
        }
    }
}
