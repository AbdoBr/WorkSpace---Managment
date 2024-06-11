using System;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkSpace___Managment.Models.BookingsModel;
using WorkSpace___Managment.Repositories.Bookings;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkSpace___Managment.Repositories.BookingsRepository
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly IMongoCollection<BookingHall> _bookingHall;
        public BookingsRepository(IMongoClient client)
        {
            var database = client.GetDatabase("WorkSpaceManagment");
            var collection = database.GetCollection<BookingHall>(nameof(BookingHall));

            _bookingHall = collection;
        }

        public async Task<BookingHall> Create(BookingHall bookingHall)
        {
            await _bookingHall.InsertOneAsync(bookingHall);
            return bookingHall;
        }

        public async Task<string> Delete(ObjectId objectId)
        {
            var filter = Builders<BookingHall>.Filter.Eq(x => x.BookingID, objectId);
            var result = await _bookingHall.DeleteOneAsync(filter);

            if (result.DeletedCount == 1)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Delete Failed";
            }
        }

        public Task<BookingHall> Get(ObjectId objectId)
        {
            var filter = Builders<BookingHall>.Filter.Eq(x => x.BookingID, objectId);
            var bookingHall = _bookingHall.Find(filter).FirstOrDefaultAsync();
            return bookingHall;
        }

        public async Task<IEnumerable<BookingHall>> GetAll()
        {
            var bookingHall = await _bookingHall.Find(_ => true).ToListAsync();
            return bookingHall;
        }

        public async Task<IEnumerable<BookingHall>> GetAllByOrganizationName(string Name)
        {
            try
            {
                var filter = Builders<BookingHall>.Filter.Eq(x => x.OrganizationName, Name);
                var bookingHall = await _bookingHall.Find(filter).ToListAsync();
                return bookingHall;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or mechanism appropriate for your application)
                Console.WriteLine($"An error occurred while retrieving the Booking Hall: {ex.Message}");
                // Optionally, rethrow or return an empty list
                return new List<BookingHall>();
            }
        }


        public async Task<BookingHall> Update(ObjectId objectId, BookingHall bookingHall)
        {
            var filter = Builders<BookingHall>.Filter.Eq(x => x.BookingID, objectId);
            var update = Builders<BookingHall>.Update
                .Set(x => x.OrganizationName, bookingHall.OrganizationName)
                .Set(x => x.Tele, bookingHall.Tele)
                .Set(x => x.Email, bookingHall.Email)
                .Set(x => x.Address, bookingHall.Address)
                .Set(x => x.HallName, bookingHall.HallName)
                .Set(x => x.BookingType, bookingHall.BookingType)
                .Set(x => x.Price, bookingHall.Price);

            var options = new FindOneAndUpdateOptions<BookingHall>
            {
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var updatedbookingHall = await _bookingHall.FindOneAndUpdateAsync(filter, update, options);
                if (updatedbookingHall == null)
                {
                    // Handle the case where the snack was not found
                    throw new Exception("Snack not found");
                }
                return updatedbookingHall;
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

