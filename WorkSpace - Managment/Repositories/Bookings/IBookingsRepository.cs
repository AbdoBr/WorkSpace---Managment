using System;
using MongoDB.Bson;
using WorkSpace___Managment.Models.BookingsModel;

namespace WorkSpace___Managment.Repositories.Bookings
{
    public interface IBookingsRepository
    {
        Task<BookingHall> Create(BookingHall bookingHall);
        Task<BookingHall> Get(ObjectId objectId);
        Task<IEnumerable<BookingHall>> GetAll();
        Task<IEnumerable<BookingHall>> GetAllByOrganizationName(String Name);

        Task<BookingHall> Update(ObjectId objectId, BookingHall bookingHall);
        Task<String> Delete(ObjectId objectId);
    }
}

