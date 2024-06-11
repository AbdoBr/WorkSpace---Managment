using System;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkSpace___Managment.Models.BookingsModel;
using WorkSpace___Managment.Repositories.Bookings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkSpace___Managment.Repositories.HallSettingRepository
{
    public class HallSettingRepository : IHallSettingRepository
    {
        private readonly IMongoCollection<HallSetting> _hallsetting;
        public HallSettingRepository(IMongoClient client)
        {
            var database = client.GetDatabase("WorkSpaceManagment");
            var collection = database.GetCollection<HallSetting>(nameof(HallSetting));

            _hallsetting = collection;
        }

        public async Task<ObjectId> Create(HallSetting hallSetting)
        {
            await _hallsetting.InsertOneAsync(hallSetting);
            return hallSetting.HallID;
        }

        public async Task<string> Delete(ObjectId objectId)
        {
            var filter = Builders<HallSetting>.Filter.Eq(x => x.HallID, objectId);
            var result = await _hallsetting.DeleteOneAsync(filter);

            if (result.DeletedCount == 1)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Delete Failed";
            }
        }

        public Task<HallSetting> Get(ObjectId objectId)
        {
            var filter = Builders<HallSetting>.Filter.Eq(x => x.HallID, objectId);
            var hallsetting = _hallsetting.Find(filter).FirstOrDefaultAsync();
            return hallsetting;
        }

        public async Task<IEnumerable<HallSetting>> GetAll()
        {
            var hallsetting = await _hallsetting.Find(_ => true).ToListAsync();
            return hallsetting;
        }

        public async Task<IEnumerable<HallSetting>> GetAllByName(string Name)
        {
            try
            {
                var filter = Builders<HallSetting>.Filter.Eq(x => x.HallName, Name);
                var hallsetting = await _hallsetting.Find(filter).ToListAsync();
                return hallsetting;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or mechanism appropriate for your application)
                Console.WriteLine($"An error occurred while retrieving the Hall: {ex.Message}");
                // Optionally, rethrow or return an empty list
                return new List<HallSetting>();
            }
        }

        public async Task<HallSetting> GetHallByNameAndTypeAsync(string hallName, string bookingType)
        {
            var filter = Builders<HallSetting>.Filter.And(
                Builders<HallSetting>.Filter.Eq(h => h.HallName, hallName),
                Builders<HallSetting>.Filter.Eq(h => h.BookingType, bookingType)
            );
            return await _hallsetting.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<HallSetting> Update(ObjectId objectId, HallSetting hallSetting)
        {
            var filter = Builders<HallSetting>.Filter.Eq(x => x.HallID, objectId);
            var update = Builders<HallSetting>.Update
                .Set(x => x.HallName, hallSetting.HallName)
                .Set(x => x.BookingType, hallSetting.BookingType)
                .Set(x => x.Price, hallSetting.Price);

            var options = new FindOneAndUpdateOptions<HallSetting>
            {
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var updatedhallsetting = await _hallsetting.FindOneAndUpdateAsync(filter, update, options);
                if (updatedhallsetting == null)
                {
                    // Handle the case where the snack was not found
                    throw new Exception("Snack not found");
                }
                return updatedhallsetting;
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller

            }
        }
    }
}
